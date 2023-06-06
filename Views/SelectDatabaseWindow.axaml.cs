using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper;

public partial class SelectDatabaseWindow : Window
{
    private ObservableCollection<string> _databasesToSelect;

    public SelectDatabaseWindow()
    {
        InitializeComponent();
        PopulateList();
    }

    private void PopulateList()
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;";
        databases = this.FindControl<ListBox>("databases");
        _databasesToSelect = new ObservableCollection<string>();
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
                    if (databaseName != null) _databasesToSelect.Add(databaseName);
                }

                databases.Items = _databasesToSelect;
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

    private void ExitApp(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void MinimizeApp(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow(true);
        mainWindow.Show();
        Close();
    }

    private void SelectButton_Click(object? sender, RoutedEventArgs e)
    {
        if (databases.SelectedItem != null)
        {
            string selectedDatabase = databases.SelectedItem.ToString()!;
            SelectTableWindow selectTableWindow = new SelectTableWindow(selectedDatabase);
            selectTableWindow.Show();
            this.Close();
        }
    }
}