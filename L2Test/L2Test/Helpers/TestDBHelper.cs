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
        public void NewQuestion(string question, string answer1, string answer2, string answer3, string answer4, string answer5, string answer6, string answer7, string answer8, bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8, string Category)
        {
            Password pass = new Password();
            string id1 = pass.Generate();
            string id2 = pass.Generate();
            string id3 = pass.Generate();
            string id4 = pass.Generate();
            string id5 = pass.Generate();
            string id6 = pass.Generate();
            string id7 = pass.Generate();
            string id8 = pass.Generate();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO Test (Question, A1, A2, A3, A4, A5, A6, A7, A8, A1ID, A2ID, A3ID, A4ID, A5ID, A6ID, A7ID, A8ID, C1, C2, C3, C4, C5, C6, C7, C8, Category) VALUES (@question, @answer1, @answer2, @answer3, @answer4, @answer5, @answer6, @answer7, @answer8, @id1, @id2, @id3, @id4, @id5, @id6, @id7, @id8, @c1, @c2, @c3, @c4, @c5, @c6, @c7, @c8, @Category)";
                    command.Parameters.AddWithValue("@question", question);
                    command.Parameters.AddWithValue("@answer1", answer1);
                    command.Parameters.AddWithValue("@answer2", answer2);
                    command.Parameters.AddWithValue("@answer3", answer3);
                    command.Parameters.AddWithValue("@answer4", answer4);
                    command.Parameters.AddWithValue("@answer5", answer5);
                    command.Parameters.AddWithValue("@answer6", answer6);
                    command.Parameters.AddWithValue("@answer7", answer7);
                    command.Parameters.AddWithValue("@answer8", answer8);
                    command.Parameters.AddWithValue("@id1", id1);
                    command.Parameters.AddWithValue("@id2", id2);
                    command.Parameters.AddWithValue("@id3", id3);
                    command.Parameters.AddWithValue("@id4", id4);
                    command.Parameters.AddWithValue("@id5", id5);
                    command.Parameters.AddWithValue("@id6", id6);
                    command.Parameters.AddWithValue("@id7", id7);
                    command.Parameters.AddWithValue("@id8", id8);
                    command.Parameters.AddWithValue("@c1", c1);
                    command.Parameters.AddWithValue("@c2", c2);
                    command.Parameters.AddWithValue("@c3", c3);
                    command.Parameters.AddWithValue("@c4", c4);
                    command.Parameters.AddWithValue("@c5", c5);
                    command.Parameters.AddWithValue("@c6", c6);
                    command.Parameters.AddWithValue("@c7", c7);
                    command.Parameters.AddWithValue("@c8", c8);
                    command.Parameters.AddWithValue("@Category", Category);

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
                            Test.answer1 = reader.GetString(reader.GetOrdinal("A1"));
                            Test.answer2 = reader.GetString(reader.GetOrdinal("A2"));
                            Test.answer3 = reader.GetString(reader.GetOrdinal("A3"));
                            Test.answer4 = reader.GetString(reader.GetOrdinal("A4"));
                            Test.answer5 = reader.GetString(reader.GetOrdinal("A5"));
                            Test.answer6 = reader.GetString(reader.GetOrdinal("A6"));
                            Test.answer7 = reader.GetString(reader.GetOrdinal("A7"));
                            Test.answer8 = reader.GetString(reader.GetOrdinal("A8"));
                            Test.aid1 = reader.GetString(reader.GetOrdinal("A1ID"));
                            Test.aid2 = reader.GetString(reader.GetOrdinal("A2ID"));
                            Test.aid3 = reader.GetString(reader.GetOrdinal("A3ID"));
                            Test.aid4 = reader.GetString(reader.GetOrdinal("A4ID"));
                            Test.aid5 = reader.GetString(reader.GetOrdinal("A5ID"));
                            Test.aid6 = reader.GetString(reader.GetOrdinal("A6ID"));
                            Test.aid7 = reader.GetString(reader.GetOrdinal("A7ID"));
                            Test.aid8 = reader.GetString(reader.GetOrdinal("A8ID"));
                            Test.c1 = (bool)reader["C1"];
                            Test.c2 = (bool)reader["C2"];
                            Test.c3 = (bool)reader["C3"];
                            Test.c4 = (bool)reader["C4"];
                            Test.c5 = (bool)reader["C5"];
                            Test.c6 = (bool)reader["C6"];
                            Test.c7 = (bool)reader["C7"];
                            Test.c8 = (bool)reader["C8"];
                            Test.category = reader.GetString(reader.GetOrdinal("Category"));

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
                            Test.answer1 = reader.GetString(reader.GetOrdinal("A1"));
                            Test.answer2 = reader.GetString(reader.GetOrdinal("A2"));
                            Test.answer3 = reader.GetString(reader.GetOrdinal("A3"));
                            Test.answer4 = reader.GetString(reader.GetOrdinal("A4"));
                            Test.answer5 = reader.GetString(reader.GetOrdinal("A5"));
                            Test.answer6 = reader.GetString(reader.GetOrdinal("A6"));
                            Test.answer7 = reader.GetString(reader.GetOrdinal("A7"));
                            Test.answer8 = reader.GetString(reader.GetOrdinal("A8"));
                            Test.aid1 = reader.GetString(reader.GetOrdinal("A1ID"));
                            Test.aid2 = reader.GetString(reader.GetOrdinal("A2ID"));
                            Test.aid3 = reader.GetString(reader.GetOrdinal("A3ID"));
                            Test.aid4 = reader.GetString(reader.GetOrdinal("A4ID"));
                            Test.aid5 = reader.GetString(reader.GetOrdinal("A5ID"));
                            Test.aid6 = reader.GetString(reader.GetOrdinal("A6ID"));
                            Test.aid7 = reader.GetString(reader.GetOrdinal("A7ID"));
                            Test.aid8 = reader.GetString(reader.GetOrdinal("A8ID"));
                            Test.c1 = (bool)reader["C1"];
                            Test.c2 = (bool)reader["C2"];
                            Test.c3 = (bool)reader["C3"];
                            Test.c4 = (bool)reader["C4"];
                            Test.c5 = (bool)reader["C5"];
                            Test.c6 = (bool)reader["C6"];
                            Test.c7 = (bool)reader["C7"];
                            Test.c8 = (bool)reader["C8"];
                            Test.category = reader.GetString(reader.GetOrdinal("Category"));

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