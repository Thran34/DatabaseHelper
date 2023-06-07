using Avalonia.Controls;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper.ViewModels
{
    internal class SelectTableViewModel
    {
        private ObservableCollection<string> _table;
        public void PopulateList(string selectedDatabase, ListBox tables)
        {
            string connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={selectedDatabase};Integrated Security=True;";

            _table = new ObservableCollection<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command =
                        new SqlCommand(
                            @"SELECT *
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo';
                ",
                            connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string tableName = reader["TABLE_NAME"].ToString();
                        _table.Add(tableName);
                    }

                    tables.Items = _table;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
