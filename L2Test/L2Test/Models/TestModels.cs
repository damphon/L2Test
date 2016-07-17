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
                sb.Append("</p><p class='TestCorrectAnswer'>A. ");
                sb.Append(Question.answer1);
                sb.Append("</p><p class='TestAnswer'>B. ");
                sb.Append(Question.answer2);
                sb.Append("</p><p class='TestAnswer'>C. ");
                sb.Append(Question.answer3);
                sb.Append("</p><p class='TestAnswer'>D. ");
                sb.Append(Question.answer4);
                sb.Append("</p><p class='TestCatagory'>Catagory: ");
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
                sb.Append("</p><p class='TestCorrectAnswer'>A. ");
                sb.Append(Question.answer1);
                sb.Append("</p><p class='TestAnswer'>B. ");
                sb.Append(Question.answer2);
                sb.Append("</p><p class='TestAnswer'>C. ");
                sb.Append(Question.answer3);
                sb.Append("</p><p class='TestAnswer'>D. ");
                sb.Append(Question.answer4);
                sb.Append("</p><p class='TestCatagory'>Catagory: ");
                sb.Append(Question.catagory);
                sb.Append("</p><div id='TestData' question='");//creates a hidden element with all game data so the JS can fill out edit form automaticly.
                sb.Append(Question.question);
                sb.Append("' answer1='");
                sb.Append(Question.answer1);
                sb.Append("' answer2='");
                sb.Append(Question.answer2);
                sb.Append("' answer3='");
                sb.Append(Question.answer3);
                sb.Append("' answer4='");
                sb.Append(Question.answer4);
                sb.Append("' catagory='");
                sb.Append(Question.catagory);
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
    }
}