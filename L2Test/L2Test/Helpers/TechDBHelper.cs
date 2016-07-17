using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using L2Test.Models;

namespace L2Test.Helpers
{
    public class TechDBHelper
    {
        public void NewID(string tech, string techID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO Login (Tech, TechID) VALUES (@tech, @techID)";
                    command.Parameters.AddWithValue("@tech", tech);
                    command.Parameters.AddWithValue("@techID", techID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch { throw; }
                }
            }
        }

        public List<TechModels> CheckID(string ID)
        {
            var TechID = new List<TechModels>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = String.Format("SELECT * FROM Login WHERE TechID = '{0}'", ID);
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Tech = new TechModels();
                            Tech.TechKey = reader.GetInt32(reader.GetOrdinal("P_Id"));
                            Tech.TechName = reader.GetString(reader.GetOrdinal("Tech"));
                            Tech.TechID = ID;
                            Tech.Time = reader.GetDateTime(reader.GetOrdinal("Time"));

                            TechID.Add(Tech);
                        }
                    }
                }
            }
            return TechID;
        }

        public List<TechModels> ListAll()
        {
            var TechID = new List<TechModels>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                connection.Open();
                string query = String.Format("SELECT * FROM Login");
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Tech = new TechModels();
                            Tech.TechKey = reader.GetInt32(reader.GetOrdinal("P_Id"));
                            Tech.TechName = reader.GetString(reader.GetOrdinal("Tech"));
                            Tech.TechID = reader.GetString(reader.GetOrdinal("TechID"));
                            Tech.Time = reader.GetDateTime(reader.GetOrdinal("Time"));

                            TechID.Add(Tech);
                        }
                    }
                }
            }
            return TechID;
        }
    }
}