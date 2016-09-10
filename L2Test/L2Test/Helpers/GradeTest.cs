using L2Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace L2Test.Helpers
{
    public class GradeTest
    {
        public float CorrectAnswers = 0f;
        public float PossibleAnswers = 0f;

        public void Grading(List<TestResultModel> TechAnswers)
        {
            string TID = TechAnswers[0].tech;
            string TName = "Unknown Tech";
            TechDBHelper Tdbhelp = new TechDBHelper();
            var TechDB = Tdbhelp.ListAll();
            ReportCardDBHelper RCdbhelp = new ReportCardDBHelper();

            foreach (var ID in TechDB)
            {
                if (ID.TechID == TID)
                    TName = ID.TechName;
            }

            string Results = PrintResults(TechAnswers);
            string URL = SaveResults(Results, TName, TID);

            RCdbhelp.NewReportCard(TName, URL);

        }

        public List<TestModels> GetTestQuestions(List<TestResultModel> TechAnswers)
        {
            List<TestModels> FullTest = TestModels.QuestionList();
            List<TestModels> TestQuestions = FullTest.ToList();
            List<string> TempString = new List<string>();

            foreach (var QID in TechAnswers)
            {
                TempString.Add(QID.question);
            }

            foreach (var Question in FullTest)
            {
                if (!(TempString.Any(x => x.Contains(Question.key.ToString()))))
                {
                    TestQuestions.Remove(Question);
                }
            }
            return TestQuestions;
        }

        public string PrintResults(List<TestResultModel> TechAnswers)
        {
            List<TestModels> TestQuestions = GetTestQuestions(TechAnswers);
            Dictionary<string, string[]> AnswerDict = TechAnswers.ToDictionary(x => x.question, x => x.answers);
            
            //Account for unanswered questions so that there are no null values and unanswered questions are marked incorrect
            foreach(var Question in TestQuestions)
            {
                if (AnswerDict[Question.key.ToString()] == null)
                {
                    AnswerDict[Question.key.ToString()] = new string[] {"Unanswed"}; //Unanswered is 8 charicters, all valid keys are 10 so this can never be a correct answer
                }
            }

            string ResultString = "";

            foreach (var Question in TestQuestions)
            {
                //0 netural, 1 correct, 2 wrong;
                int Answer1Status = 0;
                int Answer2Status = 0;
                int Answer3Status = 0;
                int Answer4Status = 0;
                PossibleAnswers++;

                //Check answeres
                //Is there a way to save a raw version of the test that was just submitted?
                foreach (var A in AnswerDict[Question.key.ToString()])// if no answer is selected null referance error
                {
                    if (Answer1Status != 1)
                    {
                        if (Question.c1 == true)
                        {
                            if (A.Contains(Question.aid1)) { Answer1Status = 1; }
                            else { Answer1Status = 2;}
                        }
                        else
                        {
                                if (A.Contains(Question.aid1)) { Answer1Status = 2; }
                        }
                    }

                    if (Answer2Status != 1)
                    {
                        if (Question.c2 == true)
                        {
                            if (A.Contains(Question.aid2)) { Answer2Status = 1; }
                            else { Answer2Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid2)) { Answer2Status = 2; }
                        }
                    }

                    if (Answer3Status != 1)
                    {
                        if (Question.c3 == true)
                        {
                            if (A.Contains(Question.aid3)) { Answer3Status = 1; }
                            else { Answer3Status = 2;  }
                        }
                        else
                        {
                            if (A.Contains(Question.aid3)) { Answer3Status = 2; }
                        }
                    }

                    if (Answer4Status != 1)
                    {
                        if (Question.c4 == true)
                        {
                            if (A.Contains(Question.aid4)) { Answer4Status = 1; }
                            else { Answer4Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid4)) { Answer4Status = 2; }
                        }
                    }
                }
                if (Answer1Status != 2 && Answer2Status != 2 && Answer3Status != 2 && Answer4Status != 2 )
                    CorrectAnswers++;

                StringBuilder sb = new StringBuilder(ResultString);
                //Question block
                sb.Append("<li class='well' id='");
                sb.Append(Question.key);
                sb.Append("'><p class='TestQuestion'>");
                sb.Append(Question.question);
                sb.Append("</p>");

                //Answer 1
                switch (Answer1Status)
                {
                    case 0: sb.Append("<p class='TestAnswer' id='"); break;
                    case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                    case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                    default: throw new Exception("Invalid Answer Status. Please grade results manualy.");
                }
                sb.Append(Question.aid1);
                sb.Append("'>A. ");
                sb.Append(Question.answer1);
                sb.Append("</p>");

                //Answer 2
                switch (Answer2Status)
                {
                    case 0: sb.Append("<p class='TestAnswer' id='"); break;
                    case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                    case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                    default: throw new Exception("Invalid Answer Status. Please grade results manualy.");
                }
                sb.Append(Question.aid2);
                sb.Append("'>B. ");
                sb.Append(Question.answer2);
                sb.Append("</p>");


                //Answer 3
                if (Question.answer3 != "")
                {
                    switch (Answer3Status)
                    {
                        case 0: sb.Append("<p class='TestAnswer' id='"); break;
                        case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                        case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                        default: throw new Exception("Invalid Answer Status. Please grade results manualy.");
                    }
                    sb.Append(Question.aid3);
                    sb.Append("'>C. ");
                    sb.Append(Question.answer3);
                    sb.Append("</p>");
                }

                //Answer 4
                if (Question.answer4 != "")
                {
                    switch (Answer4Status)
                    {
                        case 0: sb.Append("<p class='TestAnswer' id='"); break;
                        case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                        case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                        default: throw new Exception("Invalid Answer Status. Please grade results manualy.");
                    }
                    sb.Append(Question.aid4);
                    sb.Append("'>D. ");
                    sb.Append(Question.answer4);
                    sb.Append("</p>");
                }

                //End question block
                sb.Append("</li>");
                ResultString = sb.ToString();

            }
            return ResultString;
        }

        public string SaveResults(string Results, string Tech, string ID)
        {
            string WebPageString = "";
            String TestPath = HttpContext.Current.Server.MapPath("~/Tests/" + Tech + ID + ".html");
            String CssPath = HttpContext.Current.Server.MapPath("~/Content/Site.css");
            StringBuilder sb = new StringBuilder(WebPageString);

            //Head
            sb.Append("<!DOCTYPE html><html><head><meta charset='utf-8'/><meta name='viewport' content='width=device-width'/><link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css'><link rel='stylesheet' type='text/css' href='");
            sb.Append(CssPath);
            sb.Append("'><title>");
            sb.Append(Tech);
            sb.Append(" </title></head><body>");

            //Body 
            sb.Append("Possible:");
            sb.Append(PossibleAnswers);
            sb.Append("</br>Score:");
            sb.Append(CorrectAnswers);
            sb.Append("</br>Grade:");
            sb.Append((CorrectAnswers / PossibleAnswers) * 100f);
            sb.Append("%</br>Results below:");
            sb.Append("<ul class='Test' style='list-style-type:none'>");
            sb.Append(Results);
            sb.Append("</ul><script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js'></script></body></html>");

            WebPageString = sb.ToString();

            File.WriteAllText(TestPath, WebPageString);

            return TestPath;
        }
    }
}