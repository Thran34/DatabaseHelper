using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace DatabaseHelper;

public partial class SelectWholeTableWindow : Window
{
    private readonly string _selectedDatabase;
    public SelectWholeTableWindow()
    {

    }
    public SelectWholeTableWindow(string selectedTable, string selectedDatabase)
    {
        _selectedDatabase = selectedDatabase;
        InitializeComponent(selectedTable, selectedDatabase);
        this.AttachDevTools();
        PopulateDataTable(selectedTable, selectedDatabase, dataTable);
    }

    private void InitializeComponent(string selectedTable, string selectedDatabase)
    {
        AvaloniaXamlLoader.Load(this);
        dataTable = this.FindControl<DataGrid>("dataTable");
    }

    private void PopulateDataTable(string selectedTable, string selectedDatabase, DataGrid dataGrid)
    {
        var commandq = @$"SELECT * FROM {selectedTable}";
        using (var connection = new SqlConnection(@$"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={selectedDatabase};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            SqlCommand command = new SqlCommand(commandq, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            // Clearing the columns from the DataGrid before adding new ones
            dataGrid.Columns.Clear();

            // Adding columns to the DataGrid
            foreach (DataColumn column in dataTable.Columns)
            {
                DataGridColumn textColumn = new DataGridTextColumn
                {
                    Header = column.ColumnName,
                    Binding = new Binding($"[{column.ColumnName}]")

                };
                textColumn.CanUserSort = true;
                dataGrid.Columns.Add(textColumn);
            }

            var collection = new ObservableCollection<Dictionary<string, object>>();
            foreach (DataRow row in dataTable.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column].ToString()?.Trim()!);
                }
                collection.Add(rowDict);
            }

            dataGrid.Items = collection;
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
        var selectTableWindow = new SelectTableWindow(_selectedDatabase);
        selectTableWindow.Show();
        Close();
    }
}
