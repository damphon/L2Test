using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace L2Test.Helpers
{
    public class Install
    {
        public string RunInstall(string connection, string user, string password)
        {
            string Result = "Installation Results:<br/>";
             Result += SetConnectionString(connection, user, password);
             Result += SetTestDB();
             Result += SetTechDB();
             Result += SetGradeDB();
            Result += "If installation was successful please test the database";
            return Result;
        }

        private string SetConnectionString(string connection, string user, string password)
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            section.ConnectionStrings["L2TestConnection"].ConnectionString = "Data Source=" + connection + ";Initial Catalog=L2TestDB;Persist Security Info=True;User ID=" + user + ";Password="+ password;
            try
            {
                configuration.Save();
            }
            catch
            {
                return "Faild to update web.config<br/>";
            }
            return "Web.Config Updated<br/>";
        }

        private string SetTestDB()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"CREATE TABLE Test
                                            (
                                            P_Id int IDENTITY NOT NULL PRIMARY KEY,
                                            Question varchar(1024) NOT NULL,
                                            A1 varchar(255) NOT NULL,
                                            A2 varchar(255) NOT NULL,
                                            A3 varchar(255),
                                            A4 varchar(255),
                                            A5 varchar(255),
                                            A6 varchar(255),
                                            A7 varchar(255),
                                            A8 varchar(255),
                                            A1ID varchar(10) NOT NULL,
                                            A2ID varchar(10) NOT NULL,
                                            A3ID varchar(10),
                                            A4ID varchar(10),
                                            A5ID varchar(10),
                                            A6ID varchar(10),
                                            A7ID varchar(10),
                                            A8ID varchar(10),
                                            C1 BIT NOT NULL,
                                            C2 BIT NOT NULL,
                                            C3 BIT,
                                            C4 BIT,
                                            C5 BIT,
                                            C6 BIT,
                                            C7 BIT,
                                            C8 BIT,
                                            Catagory varchar(255) NOT NULL
                                            )";

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        return "The following error occured while creating the Test database: " + e.Message + "<br/>";
                    }
                } 
            }
            return "Test table created and configured<br/>";
        }

        private string SetTechDB()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"CREATE TABLE Login
                                            (
                                            P_Id int IDENTITY NOT NULL PRIMARY KEY,
                                            Tech varchar(255) NOT NULL,
                                            TechID varchar(255) NOT NULL,
                                            Time SMALLDATETIME DEFAULT (getdate())
                                            )";

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        return "The following error occured while creating the Tech database: " + e.Message + "<br/>";
                    };
                }
            }
            return "Tech table created and configured<br/>";
        }

        private string SetGradeDB()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["L2TestConnection"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"CREATE TABLE ReportCard
                                            (
                                            P_Id int IDENTITY NOT NULL PRIMARY KEY,
                                            Tech varchar(255) NOT NULL,
                                            Test varchar(255) NOT NULL,
                                            Type_Bool bit NOT NULL,
                                            Time_Stamp SMALLDATETIME DEFAULT (getdate())
                                            )";

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        return "The following error occured while creating the ReportCard database: " + e.Message + "<br/>";
                    }
                }
            }
            return "ReportCard table created and configured<br/>";
        }

        public string TestDB(string URL)
        {
            string Result = "";
            if (TestDBExists())
            {
                Result += "Test Database exists<br/>";
                if (TechDBExists())
                {
                    Result += "Tech Database exists<br/>";
                    if (ReportCardDBExists())
                    {
                        Result += "ReportCard Database exists<br/>";
                        if (UserDBExists())
                        {
                            Result += "User Management Database exists<br/>";
                            Result += "<a href='" + URL + "'> Complete  Installation</a>";
                        }
                        else
                        {
                            Result += "ERROR:User Management Database Does not exist<br/>";
                        }
                    }
                    else
                    {
                        Result += "ERROR:Report Card Database Does not exist<br/>";
                    }
                }
                else
                {
                    Result += "ERROR:Tech Database Does not exist<br/>";
                }
            }
            else
            {
                Result += "ERROR:Test Database Does not exist<br/>";
            }
           return Result;
        }

        private bool TestDBExists()
        {
            return true;
        }

        private bool TechDBExists()
        {
            return true;
        }

        private bool ReportCardDBExists()
        {
            return true;
        }
        private bool UserDBExists()
        {
            return true;
        }
    }
}
