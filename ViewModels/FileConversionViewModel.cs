using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace DatabaseHelper.ViewModels
{
    public class FileConversionViewModel : ViewModelBase
    {
        private const string ConnectionStringFormat =
            "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog =  {0}; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        private const string ExcelConnectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

        public void CreateDatabase(string folderPath, string databaseName)
        {
            try
            {
                string connectionString = string.Format(ConnectionStringFormat, databaseName);
                var files = GetFiles(folderPath);

                foreach (var file in files)
                {
                    ProcessFile(file, connectionString);
                }

                InfoWindow infoWindow = new InfoWindow("Tables succesfully created.");
                infoWindow.Show();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Sql exception: {ex}");
            }
        }

        private FileInfo[] GetFiles(string folderPath)
        {
            var directory = new DirectoryInfo(folderPath);
            return directory.GetFiles();
        }

        private void ProcessFile(FileInfo file, string connectionString)
        {
            string fileFullPath = Path.Combine(file.DirectoryName!, file.Name);
            string filename = file.Name.Replace(".xlsx", "");
            string excelConStr = string.Format(ExcelConnectionString, fileFullPath);

            using (OleDbConnection connection = new OleDbConnection(excelConStr))
            {
                connection.Open();
                DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null)!;

                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString()!.Contains("$"))
                    {
                        string sheetname = drSheet["TABLE_NAME"].ToString()!;
                        DataTable dt = GetTableData(sheetname, connection);

                        CreateTableAndImportData(dt, filename, connectionString);
                    }
                }
            }
        }

        private DataTable GetTableData(string sheetname, OleDbConnection connection)
        {
            OleDbCommand command = new OleDbCommand($"select * from [{sheetname}]", connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        private void CreateTableAndImportData(DataTable dt, string filename, string connectionString)
        {
            string tableDDL = BuildTableDDL(dt, filename);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                ExecuteNonQuery(sqlConnection, tableDDL);
                ImportData(sqlConnection, dt, filename);
            }
        }

        private string BuildTableDDL(DataTable dt, string filename)
        {
            var tableDDL = new StringBuilder();
            tableDDL.AppendFormat("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) CREATE TABLE [{0}](", filename);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tableDDL.AppendFormat("[{0}] NVarchar(max)", dt.Columns[i].ColumnName);

                if (i != dt.Columns.Count - 1)
                {
                    tableDDL.Append(",");
                }
            }

            tableDDL.Append(")");
            return tableDDL.ToString();
        }

        private void ExecuteNonQuery(SqlConnection sqlConnection, string query)
        {
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void ImportData(SqlConnection sqlConnection, DataTable dt, string tableName)
        {
            using (SqlBulkCopy blk = new SqlBulkCopy(sqlConnection))
            {
                blk.DestinationTableName = $"[{tableName}]";
                blk.WriteToServer(dt);
            }
        }
    }
}
