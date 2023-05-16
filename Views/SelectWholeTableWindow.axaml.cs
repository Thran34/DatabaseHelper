using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace DatabaseHelper;

public partial class SelectWholeTableWindow : Window
{
    private ObservableCollection<object> users;

    public SelectWholeTableWindow()
    {

    }
    public SelectWholeTableWindow(string selectedTable, string selectedDatabase)
    {
        InitializeComponent(selectedTable, selectedDatabase);
        this.AttachDevTools();
    }

    private void InitializeComponent(string selectedTable, string selectedDatabase)
    {
        AvaloniaXamlLoader.Load(this);
        users = new ObservableCollection<object>();
        dataTable = this.FindControl<DataGrid>("dataTable");
        PopulateDataTable(selectedTable, selectedDatabase);
    }

    private void PopulateDataTable(string selectedTable, string selectedDatabase)
    {
        var commandq = @$"SELECT * FROM {selectedTable}";
        using (var connection = new SqlConnection(@$"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={selectedDatabase};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            SqlCommand command = new SqlCommand(commandq, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTablee = new DataTable();
            dataTablee.Load(reader);

            List<DataGridColumn> columns = new List<DataGridColumn>();
            foreach (DataColumn column in dataTablee.Columns)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn
                {
                    Header = column.ColumnName,
                    Binding = new Avalonia.Data.Binding($"[{column.ColumnName}]")
                };
                dataTable.Columns.Add(textColumn);
            }
        }
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
