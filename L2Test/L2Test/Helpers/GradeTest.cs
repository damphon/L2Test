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
        public static int PassingScore = 85; //this is out of 100.
        public List<GradeCatagories> categoryGrades = new List<GradeCatagories>();

        public Dictionary<string, float> Grading(List<TestResultModel> TechAnswers)
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
            string URL = SaveResults(Results, TName);
            RCdbhelp.NewReportCard(TName, URL, 1);
            return FinalScore();

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
                    AnswerDict[Question.key.ToString()] = new string[] { "Unanswered" }; //Unanswered is 8 characters, all valid keys are 10 so this can never be a correct answer
                }
            }

            string ResultString = "";

            foreach (var Question in TestQuestions)
            {
                //Save status of each answer for grading and to print with correct/incorrect color scheme
                //0 neutral, 1 correct, 2 wrong;
                int Answer1Status = 0;
                int Answer2Status = 0;
                int Answer3Status = 0;
                int Answer4Status = 0;
                int Answer5Status = 0;
                int Answer6Status = 0;
                int Answer7Status = 0;
                int Answer8Status = 0;

                //Tells which answers were selected for printout
                bool Answer1Selected = false;
                bool Answer2Selected = false;
                bool Answer3Selected = false;
                bool Answer4Selected = false;
                bool Answer5Selected = false;
                bool Answer6Selected = false;
                bool Answer7Selected = false;
                bool Answer8Selected = false;

                PossibleAnswers++;

                //Check answeres
                foreach (var A in AnswerDict[Question.key.ToString()])
                {
                    if (Answer1Status != 1)
                    {
                        if (Question.c1 == true)
                        {
                            if (A.Contains(Question.aid1)) { Answer1Status = 1; Answer1Selected = true; }
                            else { Answer1Status = 2; }
                        }
                        else
                        {
                                if (A.Contains(Question.aid1)) { Answer1Status = 2; Answer1Selected = true; }
                        }
                    }

                    if (Answer2Status != 1)
                    {
                        if (Question.c2 == true)
                        {
                            if (A.Contains(Question.aid2)) { Answer2Status = 1; Answer2Selected = true; }
                            else { Answer2Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid2)) { Answer2Status = 2; Answer2Selected = true; }
                        }
                    }

                    if (Answer3Status != 1)
                    {
                        if (Question.c3 == true)
                        {
                            if (A.Contains(Question.aid3)) { Answer3Status = 1; Answer3Selected = true; }
                            else { Answer3Status = 2;  }
                        }
                        else
                        {
                            if (A.Contains(Question.aid3)) { Answer3Status = 2; Answer3Selected = true; }
                        }
                    }

                    if (Answer4Status != 1)
                    {
                        if (Question.c4 == true)
                        {
                            if (A.Contains(Question.aid4)) { Answer4Status = 1; Answer4Selected = true; }
                            else { Answer4Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid4)) { Answer4Status = 2; Answer4Selected = true; }
                        }
                    }

                    if (Answer5Status != 1)
                    {
                        if (Question.c5 == true)
                        {
                            if (A.Contains(Question.aid5)) { Answer5Status = 1; Answer5Selected = true; }
                            else { Answer5Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid5)) { Answer5Status = 2; Answer5Selected = true; }
                        }
                    }

                    if (Answer6Status != 1)
                    {
                        if (Question.c6 == true)
                        {
                            if (A.Contains(Question.aid6)) { Answer6Status = 1; Answer6Selected = true; }
                            else { Answer6Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid6)) { Answer6Status = 2; Answer6Selected = true; }
                        }
                    }

                    if (Answer7Status != 1)
                    {
                        if (Question.c7 == true)
                        {
                            if (A.Contains(Question.aid7)) { Answer7Status = 1; Answer7Selected = true; }
                            else { Answer7Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid7)) { Answer7Status = 2; Answer7Selected = true; }
                        }
                    }

                    if (Answer8Status != 1)
                    {
                        if (Question.c8 == true)
                        {
                            if (A.Contains(Question.aid8)) { Answer8Status = 1; Answer8Selected = true; }
                            else { Answer8Status = 2; }
                        }
                        else
                        {
                            if (A.Contains(Question.aid8)) { Answer8Status = 2; Answer8Selected = true; }
                        }
                    }
                }

                //If all answers are correct Add one to correct answers and update category list
                if (Answer1Status != 2 && Answer2Status != 2 && Answer3Status != 2 && Answer4Status != 2 && Answer5Status != 2 && Answer6Status != 2 && Answer7Status != 2 && Answer8Status != 2)
                {
                    CorrectAnswers++;
                    if (categoryGrades.Exists(x => x.category == Question.category))
                    {
                        foreach (var c in categoryGrades)
                        {
                            if (c.category == Question.category)
                            {
                                c.CorrectAnswers++;
                                c.PossibleAnsweres++;
                            }
                        }
                    }
                    else
                    {
                        var CatList = new GradeCatagories();
                        CatList.category = Question.category;
                        CatList.CorrectAnswers = 1f;
                        CatList.PossibleAnsweres = 1f;
                        categoryGrades.Add(CatList);
                    }
                }
                else //If this question was answered incorrectly 
                {
                    if (categoryGrades.Exists(x => x.category == Question.category))
                    {
                        foreach (var c in categoryGrades)
                        {
                            if (c.category == Question.category)
                            {
                                c.PossibleAnsweres++;
                            }
                        }
                    }
                    else
                    {
                        var CatList = new GradeCatagories();
                        CatList.category = Question.category;
                        CatList.PossibleAnsweres = 1f;
                        categoryGrades.Add(CatList);
                    }
                }

                StringBuilder sb = new StringBuilder(ResultString);
                //Question block
                sb.Append("<li class='well' id='");
                sb.Append(Question.key);
                sb.Append("'><p class='TestResultQuestion'>category:<b>");
                sb.Append(Question.category);
                sb.Append("</b> - ");
                sb.Append(Question.question);
                sb.Append("</p>");

                //Answer 1
                switch (Answer1Status)
                {
                    case 0: sb.Append("<p class='TestAnswer' id='"); break;
                    case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                    case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                    default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                }
                sb.Append(Question.aid1);
                if (Answer1Selected)
                    sb.Append("'><img src='/selected.png' height='20' width='20'>");
                else
                    sb.Append("'>");
                sb.Append("A. ");
                sb.Append(Question.answer1);
                sb.Append("</p>");

                //Answer 2
                switch (Answer2Status)
                {
                    case 0: sb.Append("<p class='TestAnswer' id='"); break;
                    case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                    case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                    default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                }
                sb.Append(Question.aid2);
                if (Answer2Selected)
                    sb.Append("'><img src='/selected.png' height='20' width='20'>");
                else
                    sb.Append("'>");
                sb.Append("B. ");
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
                        default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                    }
                    sb.Append(Question.aid3);
                    if (Answer3Selected)
                        sb.Append("'><img src='/selected.png' height='20' width='20'>");
                    else
                        sb.Append("'>");
                    sb.Append("C. ");
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
                        default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                    }
                    sb.Append(Question.aid4);
                    if (Answer4Selected)
                        sb.Append("'><img src='/selected.png' height='20' width='20'>");
                    else
                        sb.Append("'>");
                    sb.Append("D. ");
                    sb.Append(Question.answer4);
                    sb.Append("</p>");
                }
                //Answer 5
                if (Question.answer5 != "")
                {
                    switch (Answer5Status)
                    {
                        case 0: sb.Append("<p class='TestAnswer' id='"); break;
                        case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                        case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                        default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                    }
                    sb.Append(Question.aid5);
                    if (Answer5Selected)
                        sb.Append("'><img src='/selected.png' height='20' width='20'>");
                    else
                        sb.Append("'>");
                    sb.Append("E. ");
                    sb.Append(Question.answer5);
                    sb.Append("</p>");
                }

                //Answer 6
                if (Question.answer6 != "")
                {
                    switch (Answer6Status)
                    {
                        case 0: sb.Append("<p class='TestAnswer' id='"); break;
                        case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                        case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                        default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                    }
                    sb.Append(Question.aid6);
                    if (Answer6Selected)
                        sb.Append("'><img src='/selected.png' height='20' width='20'>");
                    else
                        sb.Append("'>");
                    sb.Append("F. ");
                    sb.Append(Question.answer6);
                    sb.Append("</p>");
                }
                //Answer 7
                if (Question.answer7 != "")
                {
                    switch (Answer7Status)
                    {
                        case 0: sb.Append("<p class='TestAnswer' id='"); break;
                        case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                        case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                        default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                    }
                    sb.Append(Question.aid7);
                    if (Answer7Selected)
                        sb.Append("'><img src='/selected.png' height='20' width='20'>");
                    else
                        sb.Append("'>");
                    sb.Append("G. ");
                    sb.Append(Question.answer7);
                    sb.Append("</p>");
                }

                //Answer 8
                if (Question.answer8 != "")
                {
                    switch (Answer8Status)
                    {
                        case 0: sb.Append("<p class='TestAnswer' id='"); break;
                        case 1: sb.Append("<p class='CorrectAnswer' id='"); break;
                        case 2: sb.Append("<p class='WrongAnswer' id='"); break;
                        default: throw new Exception("Invalid Answer Status. Please grade results manually.");
                    }
                    sb.Append(Question.aid8);
                    if (Answer8Selected)
                        sb.Append("'><img src='/selected.png' height='20' width='20'>");
                    else
                        sb.Append("'>");
                    sb.Append("H. ");
                    sb.Append(Question.answer8);
                    sb.Append("</p>");
                }

                //End question block
                sb.Append("</li>");
                ResultString = sb.ToString();

            }
            return ResultString;
        }

        public string SaveResults(string Results, string Tech)
        {
            string WebPageString = "";
            string Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            string FileDate = DateTime.Now.ToString("yyyy-MM-dd");
            String TestPath = HttpContext.Current.Server.MapPath("/Views/Tests/" + Tech + FileDate + ".html");
            String CssPath = "/Content/Site.css";
            StringBuilder sb = new StringBuilder(WebPageString);

            //Head
            sb.Append("<!DOCTYPE html><html><head><meta charset='utf-8'/><meta name='viewport' content='width=device-width'/><link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css'><link rel='stylesheet' type='text/css' href='");
            sb.Append(CssPath);
            sb.Append("'><title>");
            sb.Append(Tech);
            sb.Append(" - ");
            sb.Append(Date);
            sb.Append("</title></head><body>");

            //Body 
            sb.Append("<h2>Final results for ");
            sb.Append(Tech);
            sb.Append(". Test completed ");
            sb.Append(Date);
            sb.Append("</h2></br>");

            //Final Score
            sb.Append("<div class='LeadViewResults'>");
            if ((CorrectAnswers / PossibleAnswers * 100f) >= PassingScore) { sb.Append("<p class='greenResult'>"); }
            else { sb.Append("<p class='redResult'>"); }
            sb.Append("<b>Final Score: ");
            sb.Append((CorrectAnswers / PossibleAnswers * 100f));
            sb.Append("%</b></br>Questions asked:");
            sb.Append(PossibleAnswers);
            sb.Append("  Correctly Answered:");
            sb.Append(CorrectAnswers);
            sb.Append("</p>");

            //List the categorys
            foreach (var c in categoryGrades)
            {
                if ((c.CorrectAnswers / c.PossibleAnsweres * 100f) >= PassingScore) { sb.Append("<p class='greenResult'"); }
                else if ((c.CorrectAnswers / c.PossibleAnsweres * 100f) > (PassingScore - 10)) { sb.Append("<p class='yellowResult'"); }
                else { sb.Append("<p class='redResult'"); }

                sb.Append(">");
                sb.Append(c.category);
                sb.Append(": ");
                sb.Append((c.CorrectAnswers / c.PossibleAnsweres * 100f));
                sb.Append("%</br>Questions asked:");
                sb.Append(c.PossibleAnsweres);
                sb.Append("  Correctly Answered:");
                sb.Append(c.CorrectAnswers);
                sb.Append("</p>");
            }
            sb.Append("</div>");

            //Techs answered below

            sb.Append("</br></br><h2>Answers selected by the technician are preceded by a white checkmark:</h2>");
            sb.Append("<ul class='Test' style='list-style-type:none'>");
            sb.Append(Results);
            sb.Append("</ul><script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js'></script></body></html>");

            WebPageString = sb.ToString();

            File.WriteAllText(TestPath, WebPageString);

            return TestPath;
        }

        private Dictionary<string, float> FinalScore()
        {
            Dictionary<string, float> results = new Dictionary<string, float>();
            results.Add("FinalScore", (CorrectAnswers / PossibleAnswers) * 100f);
            foreach (var category in categoryGrades)
            {
                results.Add(category.category, (category.CorrectAnswers / category.PossibleAnsweres) * 100f);
            }
            return results;
        }
    }

    public class GradeCatagories
    {
       public string category { get; set; }
       public float PossibleAnsweres { get; set; }
       public float CorrectAnswers { get; set; }
    }
}