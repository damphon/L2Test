using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace L2Test.Helpers
{
    public static class Config
    {
        public static string GetString(string Value)
        {
            string Result = "";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = "SELECT " + Value + " FROM Config";
                SqlCommand Command = new SqlCommand(query, connection);
                var DBResult = Command.ExecuteScalar();
                Result = Convert.ToString(DBResult);
                connection.Close();
            }
            return Result;
        }

        public static int GetInt(string Value)
        {
            int Result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = "SELECT " + Value + " FROM Config";
                SqlCommand Command = new SqlCommand(query, connection);
                var DBResult = Command.ExecuteScalar();
                Result = Convert.ToInt32(DBResult);
                connection.Close();
            }
            return Result;
        }

        public static string GetCurrentConfig()
        {
            string L2Requirements = "", TestInstructions = "";
            int NumberOfQuestions = 0, PassingScore = 0, TimeToTakeTest = 0, TimeToStartTest = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = String.Format("SELECT * FROM Config");
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            L2Requirements = reader.GetString(reader.GetOrdinal("L2Requirements"));
                            TestInstructions = reader.GetString(reader.GetOrdinal("TestInstructions"));
                            NumberOfQuestions = reader.GetInt16(reader.GetOrdinal("NumberOfQuestions"));
                            PassingScore = reader.GetInt16(reader.GetOrdinal("PassingScore"));
                            TimeToTakeTest = reader.GetInt16(reader.GetOrdinal("TimeToTakeTest"));
                            TimeToStartTest = reader.GetInt16(reader.GetOrdinal("TimeToStartTest"));
                        }
                    }
                }
            }
            return "<u id='configHTML' L2Requirements='" + L2Requirements + "' TestInstructions='" + TestInstructions + "' NumberOfQuestions='" + NumberOfQuestions.ToString() + "' PassingScore='" + PassingScore.ToString() + "' TimeToTakeTest='" + TimeToTakeTest.ToString() + "' TimeToStartTest='" + TimeToStartTest.ToString() + "' style='display: none; '></u>";
        }

        public static void Update(string L2Requirements, string TestInstructions, int NumberOfQuestions, int PassingScore, int TimeToTakeTest, int TimeToStartTest)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Config SET L2Requirements = @Req, TestInstructions = @Ins, NumberOfQuestions = @Que, PassingScore = @Pass, TimeToTakeTest = @Take, TimeToStartTest = @Start WHERE Lock = 'X'";
                    command.Parameters.AddWithValue("@Req", L2Requirements);
                    command.Parameters.AddWithValue("@Ins", TestInstructions);
                    command.Parameters.AddWithValue("@Que", NumberOfQuestions);
                    command.Parameters.AddWithValue("@Pass", PassingScore);
                    command.Parameters.AddWithValue("@Take", TimeToTakeTest);
                    command.Parameters.AddWithValue("@Start", TimeToStartTest);

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
