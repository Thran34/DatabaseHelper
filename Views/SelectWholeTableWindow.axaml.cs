using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;

namespace DatabaseHelper;

public partial class SelectWholeTableWindow : Window
{
    private ObservableCollection<Models.User> users;

    public SelectWholeTableWindow()
    {
        InitializeComponent();
        this.AttachDevTools();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        dataTable = this.FindControl<DataGrid>("dataTable");
        users = new ObservableCollection<Models.User>();
        PopulateDataTable();

    }

    private void PopulateDataTable()
    {
        var commandq = @"SELECT * FROM USERS";
        using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseHelper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {


            SqlCommand command = new SqlCommand(commandq, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Models.User user = new Models.User
                {
                    Id = (int)reader["Id"],
                    Login = reader["Login"].ToString(),
                    Role = reader["Role"].ToString()
                    // Set other properties as needed
                };
                users.Add(user);
            }
        }

        dataTable.Items = users;
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
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void AddNewRow(object? sender, RoutedEventArgs e)
    {


    }
}
