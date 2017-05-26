using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace L2Test.Helpers
{
    public class DatabaseBackup
    {
        public string Backup(string path)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["L2TestConnection"].ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
            string backupFileName = String.Format("{0}{1}-{2}.bak", path, sqlConStrBuilder.InitialCatalog, DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                string query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'", sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return backupFileName;
        }
    }
}