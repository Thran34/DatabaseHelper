using Avalonia.Controls;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper.ViewModels
{
    internal class SelectTableViewModel
    {
        private ObservableCollection<string> _tables;

        public void PopulateList(string selectedDatabase, ListBox tables)
        {
            string connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={selectedDatabase};Integrated Security=True;";

            _tables = new ObservableCollection<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT *
                                 FROM INFORMATION_SCHEMA.TABLES
                                 WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo';";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string tableName = reader["TABLE_NAME"].ToString()!;
                        _tables.Add(tableName);
                    }

                    reader.Close();
                }

                tables.Items = _tables;
            }
            catch (Exception ex)
            {
                ErrorWindow errorWindow = new ErrorWindow("Database not found.");
                errorWindow.Show();
            }
        }
    }
}
