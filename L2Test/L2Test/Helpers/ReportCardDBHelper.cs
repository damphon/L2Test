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
        public void NewReportCard(string tech, string testURL)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO ReportCard (Tech, Test, Time) VALUES (@Name, @URL, @time)";
                    command.Parameters.AddWithValue("@Name", tech);
                    command.Parameters.AddWithValue("@URL", testURL);
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

        public List<ReportCardModels> GetReportCards()
        {
            var ReportCards = new List<ReportCardModels>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = "SELECT * FROM ReportCard";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Report = new ReportCardModels();
                            Report.tech = reader.GetString(reader.GetOrdinal("Tech"));
                            Report.testURL = reader.GetString(reader.GetOrdinal("Test"));
                            Report.time = reader.GetDateTime(reader.GetOrdinal("Time"));

                            ReportCards.Add(Report);
                        }
                    }
                }
            }
            return ReportCards;
        }
    }
}