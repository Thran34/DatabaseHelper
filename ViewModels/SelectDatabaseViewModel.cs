using Avalonia.Controls;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper.ViewModels
{
    internal class SelectDatabaseViewModel
    {
        public void SelectDatabase(ListBox databases, bool shouldOpenFileConversion)
        {
            if (databases.SelectedItem != null)
            {
                string selectedDatabase = databases.SelectedItem.ToString()!;
                if (shouldOpenFileConversion)
                {
                    FileConversionWindow fileConversionWindow = new FileConversionWindow(selectedDatabase);
                    fileConversionWindow.Show();
                }

                else
                {
                    SelectTableWindow selectTableWindow = new SelectTableWindow(selectedDatabase);
                    selectTableWindow.Show();
                }
            }
        }

        public void PopulateList(ListBox databases)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;";

            var databasesToSelect = new ObservableCollection<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command =
                        new SqlCommand(
                            "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')",
                            connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string? databaseName = reader["name"].ToString();
                        if (databaseName != null) databasesToSelect.Add(databaseName);
                    }

                    databases.Items = databasesToSelect;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
