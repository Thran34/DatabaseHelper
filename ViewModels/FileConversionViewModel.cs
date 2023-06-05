using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace DatabaseHelper.ViewModels
{
    public class FileConversionViewModel : ViewModelBase
    {
        public void CreateDatabase(string folderPath)
        {
            try
            {
                string databaseName = "TESTY";
                string SQLServerName = "(local)";

                SqlConnection SQLConnection = new SqlConnection();
                SQLConnection.ConnectionString =
                    @$"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog =  {databaseName}; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

                var directory = new DirectoryInfo(folderPath);
                FileInfo[] files = directory.GetFiles();

                string fileFullPath = "";

                foreach (FileInfo file in files)
                {
                    string filename = "";
                    fileFullPath = folderPath + "\\" + file.Name;
                    filename = file.Name.Replace(".xlsx", "");

                    string ConStr;
                    string HDR;
                    HDR = "YES";
                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullPath
                                                                              + ";Extended Properties=\"Excel 12.0;HDR=" +
                                                                              HDR + ";IMEX=1\"";
                    OleDbConnection cnn = new OleDbConnection(ConStr);

                    //Get Sheet Name
                    cnn.Open();
                    DataTable dtSheet = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetname;
                    sheetname = "";
                    foreach (DataRow drSheet in dtSheet.Rows)
                    {
                        if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                        {
                            sheetname = drSheet["TABLE_NAME"].ToString();
                            //Load DataTable with Sheet Data
                            OleDbCommand oconn = new OleDbCommand("select * from [" + sheetname + "]", cnn);
                            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);


                            string tableDDL = "";
                            tableDDL += "IF Not EXISTS (SELECT * FROM sys.objects WHERE object_id = ";
                            tableDDL += "OBJECT_ID(N'[dbo].[" + filename + "]') AND type in (N'U'))";
                            tableDDL += "Create table [" + filename + "]";
                            tableDDL += "(";
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (i != dt.Columns.Count - 1)
                                    tableDDL += "[" + dt.Columns[i].ColumnName + "] " + "NVarchar(max)" + ",";
                                else
                                    tableDDL += "[" + dt.Columns[i].ColumnName + "] " + "NVarchar(max)";
                            }

                            tableDDL += ")";


                            SQLConnection.Open();
                            SqlCommand myCommand = new SqlCommand(tableDDL, SQLConnection);
                            myCommand.ExecuteNonQuery();

                            SqlBulkCopy blk = new SqlBulkCopy(SQLConnection);
                            blk.DestinationTableName = "[" + filename + "]";
                            blk.WriteToServer(dt);
                        }

                        SQLConnection.Close();
                    }
                }
            }
            catch (Exception exception)
            {

            }
        }
    }
}
