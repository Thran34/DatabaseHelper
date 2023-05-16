using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper;

public partial class SelectTableWindow : Window
{
    private ObservableCollection<string> tb;

    public SelectTableWindow()
    {

    }
    public SelectTableWindow(string databaseName)
    {
        InitializeComponent();
        PopulateList(databaseName);
    }

    private void PopulateList(string databaseName)
    {
        string connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={databaseName};Integrated Security=True;";
        tables = this.FindControl<ListBox>("tables");
        tb = new ObservableCollection<string>();
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
                    tb.Add(tableName);
                }

                tables.Items = tb;
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
    private void AddNewTable(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
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

    private void SelectButton_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}