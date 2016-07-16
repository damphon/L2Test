using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using L2Test.Models;

namespace L2Test.Helpers
{
    public class ReportCardDBHelper
    {
        public string NewReportCard(string tech, string testURL)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO ReportCard (Tech, Test) VALUES (@Name, @URL)";
                    command.Parameters.AddWithValue("@Name", tech);
                    command.Parameters.AddWithValue("@URL", testURL);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        return "Error: " + e.Message;
                    }
                }
            }
            return "Report Card Saved";
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

                            ReportCards.Add(Report);
                        }
                    }
                }
            }
            return ReportCards;
        }

        public List<ReportCardModels> GetReportCard()
        {
            var ReportCard = new List<ReportCardModels>();
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

                            ReportCard.Add(Report);
                        }
                    }
                }
            }
            return ReportCard;
        }
    }
}