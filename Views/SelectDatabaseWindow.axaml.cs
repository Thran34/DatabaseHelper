using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper;

public partial class SelectDatabaseWindow : Window
{
    private ObservableCollection<string> datab;

    public SelectDatabaseWindow()
    {
        InitializeComponent();
        PopulateList();
    }

    private void PopulateList()
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;";
        databases = this.FindControl<ListBox>("databases");
        datab = new ObservableCollection<string>();
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
                    string databaseName = reader["name"].ToString();
                    datab.Add(databaseName);
                }

                databases.Items = datab;
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MinimizeApp(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ExitApp(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AddNewDatabase(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }


    private void Databases_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ListBox listBox = (ListBox)sender;
        string selectedDatabase = listBox.SelectedItem.ToString();

        // Process the selected database further
        // ...
        Console.WriteLine();
    }

    private void SelectButton_Click(object? sender, RoutedEventArgs e)
    {
        if (databases.SelectedItem != null)
        {
            string selectedDatabase = databases.SelectedItem.ToString();

            // Process the selected database further
            // ...

            // Example: Display the selected database in a MessageBox

        }
    }
}