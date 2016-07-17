using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using L2Test.Models;

namespace L2Test.Helpers
{
    public class TestDBHelper
    {
        public void NewQuestion(string question, string answer1, string answer2, string answer3, string answer4, string catagory)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO Test (Question, Answer1, Answer2, Answer3, Answer4, Catagory) VALUES (@question, @answer1, @answer2, @answer3, @answer4, @catagory)";
                    command.Parameters.AddWithValue("@question", question);
                    command.Parameters.AddWithValue("@answer1", answer1);
                    command.Parameters.AddWithValue("@answer2", answer2);
                    command.Parameters.AddWithValue("@answer3", answer3);
                    command.Parameters.AddWithValue("@answer4", answer4);
                    command.Parameters.AddWithValue("@catagory", catagory);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch { throw; }
                }
            }
        }

        public List<TestModels> GetQuestions()
        {
            var Questions = new List<TestModels>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = "SELECT * FROM Test";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Test = new TestModels();
                            Test.key = reader.GetInt32(reader.GetOrdinal("P_Id"));
                            Test.question = reader.GetString(reader.GetOrdinal("Question"));
                            Test.answer1 = reader.GetString(reader.GetOrdinal("Answer1"));
                            Test.answer2 = reader.GetString(reader.GetOrdinal("Answer2"));
                            Test.answer3 = reader.GetString(reader.GetOrdinal("Answer3"));
                            Test.answer4 = reader.GetString(reader.GetOrdinal("Answer4"));
                            Test.catagory = reader.GetString(reader.GetOrdinal("Catagory"));

                            Questions.Add(Test);
                        }
                    }
                }
            }
            return Questions;
        }

        public List<TestModels> GetQuestion(int key)
        {
            var Question = new List<TestModels>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = String.Format("SELECT * FROM Test WHERE P_Id = '{0}'", key);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Test = new TestModels();
                            Test.key = reader.GetInt32(reader.GetOrdinal("P_Id"));
                            Test.question = reader.GetString(reader.GetOrdinal("Question"));
                            Test.answer1 = reader.GetString(reader.GetOrdinal("Answer1"));
                            Test.answer2 = reader.GetString(reader.GetOrdinal("Answer2"));
                            Test.answer3 = reader.GetString(reader.GetOrdinal("Answer3"));
                            Test.answer4 = reader.GetString(reader.GetOrdinal("Answer4"));
                            Test.catagory = reader.GetString(reader.GetOrdinal("Catagory"));

                            Question.Add(Test);
                        }
                    }
                }
            }
            return Question;
        }

        public void RemoveQuestion(int key)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM Test WHERE P_Id = @Key";
                    command.Parameters.AddWithValue("@Key", key);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch { throw; }
                }
            }
        }
    }
}