using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L2Test.Helpers;
using System.Text;
using System.IO;

namespace L2Test.Models
{
    public class TestModels
    {
        public int key { get; set; }
        public string question { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string answer5 { get; set; }
        public string answer6 { get; set; }
        public string answer7 { get; set; }
        public string answer8 { get; set; }
        public string aid1 { get; set; }
        public string aid2 { get; set; }
        public string aid3 { get; set; }
        public string aid4 { get; set; }
        public string aid5 { get; set; }
        public string aid6 { get; set; }
        public string aid7 { get; set; }
        public string aid8 { get; set; }
        public bool? c1 { get; set; } //0 = correct //1 = incorrect
        public bool? c2 { get; set; }
        public bool? c3 { get; set; }
        public bool? c4 { get; set; }
        public bool? c5 { get; set; }
        public bool? c6 { get; set; }
        public bool? c7 { get; set; }
        public bool? c8 { get; set; }
        public string category { get; set; }

        public static List<TestModels> QuestionList()
        {
            TestDBHelper dbhelp = new TestDBHelper();
            List<TestModels> FullList = dbhelp.GetQuestions();
            return FullList;
        }

        public static List<TestModels> SingleQuestion(string uid)
        {
            int id = Int32.Parse(uid);
            TestDBHelper dbhelp = new TestDBHelper();
            List<TestModels> Question = dbhelp.GetQuestion(id);
            return Question;
        }

        public string EditListString()
        {
            var List = QuestionList();
            List.Reverse();
            string QuestionString = "";
            foreach (var Question in List)
            {
                StringBuilder sb = new StringBuilder(QuestionString);
                sb.Append("<li class='well'><a href='/Home/EditQuestion/");
                sb.Append(Question.key);
                sb.Append("' class='TestEdit'>Edit</a><p class='TestQuestion'>");
                sb.Append(Question.question);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c1);
                sb.Append("'>A. ");
                sb.Append(Question.answer1);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c2);
                sb.Append("'>B. ");
                sb.Append(Question.answer2);
                sb.Append("</p>");

                if (Question.answer3 != "") { 
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c3);
                    sb.Append("'>C. ");
                    sb.Append(Question.answer3);
                    sb.Append("</p>");
                }

                if (Question.answer4 != "")
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c4);
                    sb.Append("'>D. ");
                    sb.Append(Question.answer4);
                    sb.Append("</p>");
                }
                if (Question.answer5 != "")
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c5);
                    sb.Append("'>C. ");
                    sb.Append(Question.answer5);
                    sb.Append("</p>");
                }

                if (Question.answer6 != "")
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c6);
                    sb.Append("'>D. ");
                    sb.Append(Question.answer6);
                    sb.Append("</p>");
                }
                if (Question.answer7 != "")
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c7);
                    sb.Append("'>C. ");
                    sb.Append(Question.answer7);
                    sb.Append("</p>");
                }

                if (Question.answer8 != "")
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c8);
                    sb.Append("'>D. ");
                    sb.Append(Question.answer8);
                    sb.Append("</p>");
                }


                sb.Append("<p class='Testcategory'>category: ");
                sb.Append(Question.category);
                sb.Append("</p>");
                QuestionString = sb.ToString();
            }
            return QuestionString;
        }

        public string SingleQuestionString(string uid)
        {
            var List = SingleQuestion(uid);
            string QuestionString = "";
            foreach (var Question in List)
            {
                StringBuilder sb = new StringBuilder(QuestionString);
                sb.Append("<li class='well'><p class='TestQuestion'>");
                sb.Append(Question.question);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c1);
                sb.Append("'>A. ");
                sb.Append(Question.answer1);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c2);
                sb.Append("'>B. ");
                sb.Append(Question.answer2);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c3);
                sb.Append("'>C. ");
                sb.Append(Question.answer3);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c4);
                sb.Append("'>D. ");
                sb.Append(Question.answer4);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c5);
                sb.Append("'>E. ");
                sb.Append(Question.answer5);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c6);
                sb.Append("'>F. ");
                sb.Append(Question.answer6);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c7);
                sb.Append("'>G. ");
                sb.Append(Question.answer7);
                sb.Append("</p><p class='TestAnswer");
                sb.Append(Question.c8);
                sb.Append("'>H. ");
                sb.Append(Question.answer8);
                sb.Append("</p>");
                sb.Append("<p class='Testcategory'>category: ");
                sb.Append(Question.category);
                sb.Append("</p>");
                //creates a hidden element with all game data so the JS can fill out edit form automatically.
                sb.Append("<div id='TestData' question='");
                sb.Append(Question.question);
                sb.Append("' answer1='");
                sb.Append(Question.answer1);
                sb.Append("' answer2='");
                sb.Append(Question.answer2);
                sb.Append("' correct1='");
                sb.Append(Question.c1);
                sb.Append("' correct2='");
                sb.Append(Question.c2);
                sb.Append("' category='");
                sb.Append(Question.category);

                if (Question.answer3 != null)
                {
                    sb.Append("' answer3='");
                    sb.Append(Question.answer3);
                    sb.Append("' correct3='");
                    sb.Append(Question.c3);
                }

                if (Question.answer4 != null)
                {
                    sb.Append("' answer4='");
                    sb.Append(Question.answer4);
                    sb.Append("' correct4='");
                    sb.Append(Question.c4);
                }
                if (Question.answer5 != null)
                {
                    sb.Append("' answer5='");
                    sb.Append(Question.answer5);
                    sb.Append("' correct5='");
                    sb.Append(Question.c5);
                }
                if (Question.answer6 != null)
                {
                    sb.Append("' answer6='");
                    sb.Append(Question.answer6);
                    sb.Append("' correct6='");
                    sb.Append(Question.c6);
                }
                if (Question.answer7 != null)
                {
                    sb.Append("' answer7='");
                    sb.Append(Question.answer7);
                    sb.Append("' correct7='");
                    sb.Append(Question.c7);
                }
                if (Question.answer8 != null)
                {
                    sb.Append("' answer8='");
                    sb.Append(Question.answer8);
                    sb.Append("' correct8='");
                    sb.Append(Question.c8);
                }

                sb.Append("'' style='display: none;'/></li>");
                QuestionString = sb.ToString();
            }
            return QuestionString;
        }

        public List<string> categoryArray()
        {
            var List = QuestionList();
            List<string> categorys = new List<string>();
            foreach(var Question in List)
            {
                if(categorys.Contains(Question.category) == false)
                {
                    categorys.Add(Question.category);
                }
            }
            return categorys;
        }

        public string CategoryList()
        {
            var List = categoryArray();
            string categoryString = "";
            foreach (var category in List)
            {
                StringBuilder sb = new StringBuilder(categoryString);
                sb.Append("<option value='");
                sb.Append(category);
                sb.Append("'>");
                sb.Append(category);
                sb.Append("</option>");
                categoryString = sb.ToString();
            }
            return categoryString;
        }

        public static void Delete(string uid)
        {
            int id = Int32.Parse(uid);
            TestDBHelper dbhelp = new TestDBHelper();
            dbhelp.RemoveQuestion(id);
        }

        public string TakeTest()
        {
            var List = QuestionList();
            Random rnd = new Random();
            var Test = List.OrderBy(x => rnd.Next()).ToList().Take(100);
            string QuestionString = "";
            foreach (var Question in Test)
            {
                List<SortModel> SortedAnswers = new List<SortModel>();

                SortedAnswers.Add(new SortModel(Question.answer1, Question.aid1));
                SortedAnswers.Add(new SortModel(Question.answer2, Question.aid2));
                if (Question.answer3 != "")
                    SortedAnswers.Add(new SortModel(Question.answer3, Question.aid3));
                if (Question.answer4 != "")
                    SortedAnswers.Add(new SortModel(Question.answer4, Question.aid4));
                if (Question.answer5 != "")
                    SortedAnswers.Add(new SortModel(Question.answer5, Question.aid5));
                if (Question.answer6 != "")
                    SortedAnswers.Add(new SortModel(Question.answer6, Question.aid6));
                if (Question.answer7 != "")
                    SortedAnswers.Add(new SortModel(Question.answer7, Question.aid7));
                if (Question.answer8 != "")
                    SortedAnswers.Add(new SortModel(Question.answer8, Question.aid8));

                SortedAnswers = SortedAnswers.OrderBy(x => rnd.Next()).ToList();
                //Build String.
                StringBuilder sb = new StringBuilder(QuestionString);
                sb.Append("<li class='well' id='");
                sb.Append(Question.key);
                sb.Append("'><p class='TestQuestion'>");
                sb.Append(Question.question);
                sb.Append("</p>");

                sb.Append("<p class='TestAnswer' id='");
                sb.Append(SortedAnswers[0].answerKey);
                sb.Append("'>A. ");
                sb.Append(SortedAnswers[0].answer);
                sb.Append("</p>");

                sb.Append("<p class='TestAnswer' id='");
                sb.Append(SortedAnswers[1].answerKey);
                sb.Append("'>B. ");
                sb.Append(SortedAnswers[1].answer);
                sb.Append("</p>");

                if (Question.answer3 != "")
                {
                    sb.Append("<p class='TestAnswer' id='");
                    sb.Append(SortedAnswers[2].answerKey);
                    sb.Append("'>C. ");
                    sb.Append(SortedAnswers[2].answer);
                    sb.Append("</p>");
                }

                if (Question.answer4 != "")
                {
                    sb.Append("<p class='TestAnswer' id='");
                    sb.Append(SortedAnswers[3].answerKey);
                    sb.Append("'>D. ");
                    sb.Append(SortedAnswers[3].answer);
                    sb.Append("</p>");
                }
                if (Question.answer5 != "")
                {
                    sb.Append("<p class='TestAnswer' id='");
                    sb.Append(SortedAnswers[4].answerKey);
                    sb.Append("'>C. ");
                    sb.Append(SortedAnswers[4].answer);
                    sb.Append("</p>");
                }
                if (Question.answer6 != "")
                {
                    sb.Append("<p class='TestAnswer' id='");
                    sb.Append(SortedAnswers[5].answerKey);
                    sb.Append("'>D. ");
                    sb.Append(SortedAnswers[5].answer);
                    sb.Append("</p>");
                }
                if (Question.answer7 != "")
                {
                    sb.Append("<p class='TestAnswer' id='");
                    sb.Append(SortedAnswers[6].answerKey);
                    sb.Append("'>C. ");
                    sb.Append(SortedAnswers[6].answer);
                    sb.Append("</p>");
                }
                if (Question.answer8 != "")
                {
                    sb.Append("<p class='TestAnswer' id='");
                    sb.Append(SortedAnswers[7].answerKey);
                    sb.Append("'>D. ");
                    sb.Append(SortedAnswers[7].answer);
                    sb.Append("</p>");
                }

                sb.Append("</li>");
                QuestionString = sb.ToString();
            }
            return QuestionString;
        }

    }

    public class SortModel
    {
        public string answer { get; set; }
        public string answerKey { get; set; }

        public SortModel(string answer1, string aid1)
        {
            this.answer = answer1;
            this.answerKey = aid1;
        }
    }

    public class TestResultModel
    {
        public string tech { get; set; }
        public string question { get; set; }
        public string[] answers { get; set; }
        public static int PassingScore = 85;

        public static string Submit(IEnumerable<TestResultModel> json)
        {
            List<TestResultModel> TechsAnswers = new List<TestResultModel>();
            GradeTest TestHelp = new GradeTest();
            string grades = "";
            foreach (var answer in 
                json)
            {
                TechsAnswers.Add(answer);
            }
            Dictionary<string, float> results = TestHelp.Grading(TechsAnswers);
            StringBuilder sb = new StringBuilder(grades);

            sb.Append("<div class='TechViewResults'>");
            foreach(var grade in results)
            {   //Color the results
                if (grade.Value >= PassingScore)  {sb.Append("<p class='greenResult'");}
                else if (grade.Value > PassingScore - 10) {sb.Append("<p class='yellowResult'");}
                else {sb.Append("<p class='redResult'");}

                //Change font size for overall score
                if (grade.Key == "FinalScore")
                    sb.Append("style='font-size:3em'");

                //Finish listing the category scores
                sb.Append(">");
                sb.Append(grade.Key);
                sb.Append(": ");
                sb.Append(grade.Value);
                sb.Append("%</p>");

            }
            sb.Append("</br><center><H3>A copy of the test has been saved for management.</H3></center></div>");
            grades = sb.ToString();

            return grades;
        }

        //Save a copy of the test as it was when the submit button was pressed, prior to grading.
        internal static void Archive(string html, string tech)
        {
            string TName = "Unknown Tech";
            TechDBHelper Tdbhelp = new TechDBHelper();
            var TechDB = Tdbhelp.ListAll();

            foreach (var ID in TechDB)
            {
                if (ID.TechID == tech)
                    TName = ID.TechName;
            }

            string WebPageString = "";
            string FileDate = DateTime.Now.ToString("yyyy-MM-dd");
            String TestPath = HttpContext.Current.Server.MapPath("/Views/Tests/Ungraded/" + FileDate + "_" + TName + ".html");
            String CssPath = HttpContext.Current.Server.MapPath("/Content/Site.css");
            StringBuilder sb = new StringBuilder(WebPageString);

            sb.Append("<!DOCTYPE html><html><head><meta charset='utf-8'/><meta name='viewport' content='width=device-width'/><link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css'><link rel='stylesheet' type='text/css' href='");
            sb.Append(CssPath);
            sb.Append("'><title>L2 Test</title></head><body>");
            sb.Append(html);
            sb.Append("</body></html>");

            WebPageString = sb.ToString();

            File.WriteAllText(TestPath, WebPageString);
        }
    }
}