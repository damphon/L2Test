using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L2Test.Helpers;
using System.Text;

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
        public string aid1 { get; set; }
        public string aid2 { get; set; }
        public string aid3 { get; set; }
        public string aid4 { get; set; }
        public bool? c1 { get; set; } //0 = correct //1 = incorrect
        public bool? c2 { get; set; }
        public bool? c3 { get; set; }
        public bool? c4 { get; set; }
        public string catagory { get; set; }

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


                sb.Append("<p class='TestCatagory'>Catagory: ");
                sb.Append(Question.catagory);
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
                sb.Append("</p>");

                if (Question.answer3 != null)
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c3);
                    sb.Append("'>C. ");
                    sb.Append(Question.answer3);
                    sb.Append("</p>");
                }

                if (Question.answer4 != null)
                {
                    sb.Append("</p><p class='TestAnswer");
                    sb.Append(Question.c4);
                    sb.Append("'>C. ");
                    sb.Append(Question.answer4);
                    sb.Append("</p>");
                }

                sb.Append("<p class='TestCatagory'>Catagory: ");
                sb.Append(Question.catagory);
                sb.Append("</p>");
                //creates a hidden element with all game data so the JS can fill out edit form automaticly.
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
                sb.Append("' catagory='");
                sb.Append(Question.catagory);

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

                sb.Append("'' style='display: none;'/></li>");
                QuestionString = sb.ToString();
            }
            return QuestionString;
        }

        public List<string> CatagoryArray()
        {
            var List = QuestionList();
            List<string> Catagorys = new List<string>();
            foreach(var Question in List)
            {
                if(Catagorys.Contains(Question.catagory) == false)
                {
                    Catagorys.Add(Question.catagory);
                }
            }
            return Catagorys;
        }

        public string CatagoryList()
        {
            var List = CatagoryArray();
            string CatagoryString = "";
            foreach (var Catagory in List)
            {
                StringBuilder sb = new StringBuilder(CatagoryString);
                sb.Append("<option value='");
                sb.Append(Catagory);
                sb.Append("'>");
                sb.Append(Catagory);
                sb.Append("</option>");
                CatagoryString = sb.ToString();
            }
            return CatagoryString;
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
        public string[] answer { get; set; }

        public static void Submit(IEnumerable<TestResultModel> json)
        {
            foreach (var answer in json)
            {
                //Do a thing
            }
        }
    }
}