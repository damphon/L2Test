using L2Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace L2Test.Helpers
{
    public class GradeTest
    {
        public int CorrectAnswers = 0;
        public int PossibleAnswers = 0;

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

            foreach(var QID in TechAnswers)
            {
                TempString.Add(QID.question);
            }

            foreach(var Question in FullTest)
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
            Dictionary<string, string[]> AnswerDict = TechAnswers.ToDictionary(x => x.question, x => x.answer); //string[] remains null

            //foreach (var question in TechAnswers)
            //{
            //    List<string> answerList = new List<string>();
            //    foreach(var answer in question.answer)
            //    {
            //        answerList.Add(answer);
            //    }
            //    AnswerDict.Add(question.question, answerList.ToArray());
            //}

            string ResultString = "";

            foreach (var Question in TestQuestions)
            {
                bool AllAnswersCorrect = true;
                //0 netural, 1 correct, 2 wrong;
                int Answer1Status = 0;
                int Answer2Status = 0;
                int Answer3Status = 0;
                int Answer4Status = 0;
                PossibleAnswers++;

                //Check answeres
                foreach(var A in AnswerDict[Question.key.ToString()])
                {
                    if (Question.c1 == true)
                    {
                        if (A.Contains(Question.aid1)) { Answer1Status = 1; }
                        else { Answer1Status = 2; AllAnswersCorrect = false; }
                    }
                    else
                    {
                        if (A.Contains(Question.aid1)) { Answer1Status = 2; AllAnswersCorrect = false; }
                        else { Answer1Status = 0; }
                    }

                    if (Question.c2 == true)
                    {
                        if (A.Contains(Question.aid2)) { Answer2Status = 1; }
                        else { Answer2Status = 2; AllAnswersCorrect = false; }
                    }
                    else
                    {
                        if (A.Contains(Question.aid2)) { Answer2Status = 2; AllAnswersCorrect = false; }
                        else { Answer2Status = 0; }
                    }

                    if (Question.c3 == true)
                    {
                        if (A.Contains(Question.aid3)) { Answer3Status = 1; }
                        else { Answer3Status = 2; AllAnswersCorrect = false; }
                    }
                    else
                    {
                        if (A.Contains(Question.aid3)) { Answer3Status = 2; AllAnswersCorrect = false; }
                        else { Answer3Status = 0; }
                    }

                    if (Question.c4 == true)
                    {
                        if (A.Contains(Question.aid4)) { Answer4Status = 1; }
                        else { Answer4Status = 2; AllAnswersCorrect = false; }
                    }
                    else
                    {
                        if (A.Contains(Question.aid4)) { Answer4Status = 2; AllAnswersCorrect = false; }
                        else { Answer4Status = 0; }
                    }
                }

                if (AllAnswersCorrect)
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
                sb.Append("<p class='TestAnswer' id='");
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

            StringBuilder sb = new StringBuilder(WebPageString);

            //Head
            sb.Append("<!DOCTYPE html><html><head><meta charset='utf-8'/><meta name='viewport' content='width=device-width'/> @Styles.Render('~/Content/css') @Scripts.Render('~/bundles/modernizr') <title> ");
            sb.Append(Tech);
            sb.Append(" </title></head><body>");

            //Body 
            sb.Append("Possible:");
            sb.Append(PossibleAnswers);
            sb.Append("<\br>Score:");
            sb.Append(CorrectAnswers);
            sb.Append("<\br>Grade:");
            sb.Append(CorrectAnswers/PossibleAnswers);
            sb.Append("%<\br>Results below:");
            sb.Append("<div><ul class='Test' style='list-style-type:none'>");
            sb.Append(Results);
            sb.Append("</ul></div> @Scripts.Render('~/bundles/jquery') @Scripts.Render('~/bundles/bootstrap')</body></html>");

            WebPageString = sb.ToString();

            string path = "~/Tests/" + Tech + ID;
            System.IO.File.WriteAllText(path, WebPageString);

            return path;
        }
    }
}