using Avalonia.Controls;
using DatabaseHelper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

internal class SelectDatabaseViewModel
{
    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;";

    public SelectDatabaseViewModel()
    {

    }
    public void SelectDatabase(ListBox databases, bool shouldOpenFileConversion)
    {
        if (databases.SelectedItem != null)
        {
            string selectedDatabase = databases.SelectedItem.ToString()!;
            if (shouldOpenFileConversion)
            {
                OpenFileConversionWindow(selectedDatabase);
            }
            else
            {
                OpenSelectTableWindow(selectedDatabase);
            }
        }
    }

    private void OpenFileConversionWindow(string selectedDatabase)
    {
        FileConversionWindow fileConversionWindow = new FileConversionWindow(selectedDatabase);
        fileConversionWindow.Show();
    }

    private void OpenSelectTableWindow(string selectedDatabase)
    {
        SelectTableWindow selectTableWindow = new SelectTableWindow(selectedDatabase);
        selectTableWindow.Show();
    }

    public void PopulateList(ListBox databases)
    {
        var databasesToSelect = new ObservableCollection<string>();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string databaseName = reader["name"].ToString()!;
                    databasesToSelect.Add(databaseName);
                }

                reader.Close();
            }

            databases.Items = databasesToSelect;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
