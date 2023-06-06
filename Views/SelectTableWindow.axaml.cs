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

    private string _selectedDatabase;
    public SelectTableWindow()
    {

    }
    public SelectTableWindow(string selectedDatabase)
    {
        _selectedDatabase = selectedDatabase;
        InitializeComponent();
        PopulateList(selectedDatabase);
    }

    private void PopulateList(string selectedDatabase)
    {
        string connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={selectedDatabase};Integrated Security=True;";
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

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        var selectDatabaseWindow = new SelectDatabaseWindow();
        selectDatabaseWindow.Show();
        Close();
    }

    private void ExitApp(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void MinimizeApp(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void SelectButton_Click(object? sender, RoutedEventArgs e)
    {
        if (tables.SelectedItem != null)
        {
            string selectedTable = tables.SelectedItem.ToString();
            SelectWholeTableWindow selectTableWindow = new SelectWholeTableWindow(selectedTable, _selectedDatabase);
            selectTableWindow.Show();

            this.Close();
        }
    }
}