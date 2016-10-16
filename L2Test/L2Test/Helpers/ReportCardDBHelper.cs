using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using L2Test.Models;
using System;

namespace L2Test.Helpers
{
    public class ReportCardDBHelper
    {
        public void NewReportCard(string tech, string testURL, int graded)//Bool is based on if graded, True is graded and False is not graded
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO ReportCard (Tech, Test, Type_Bool, Time_Stamp) VALUES (@Name, @URL, @type, @time)";
                    command.Parameters.AddWithValue("@Name", tech);
                    command.Parameters.AddWithValue("@URL", testURL);
                    command.Parameters.AddWithValue("@type", graded);
                    command.Parameters.AddWithValue("@time", DateTime.Now);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch { throw; }
                }
            }
        }

        public List<ReportCardModels> GetReportCards(int graded)
        {
            var ReportCards = new List<ReportCardModels>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = String.Format("SELECT * FROM ReportCard WHERE Type_Bool = {0}", graded);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Report = new ReportCardModels();
                            Report.tech = reader.GetString(reader.GetOrdinal("Tech"));
                            Report.testURL = reader.GetString(reader.GetOrdinal("Test"));
                            Report.time = reader.GetDateTime(reader.GetOrdinal("Time_Stamp"));

                            ReportCards.Add(Report);
                        }
                    }
                }
            }
            return ReportCards;
        }
    }
}