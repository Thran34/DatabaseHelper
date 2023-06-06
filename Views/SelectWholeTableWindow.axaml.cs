using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace DatabaseHelper;

public partial class SelectWholeTableWindow : Window
{
    public SelectWholeTableWindow()
    {

    }
    public SelectWholeTableWindow(string selectedTable, string selectedDatabase)
    {
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

            // Convert DataTable to ObservableCollection<Dictionary<string, object>>
            var collection = new ObservableCollection<CustomRow>();
            foreach (DataRow row in dataTable.Rows)
            {
                var rowDict = new CustomRow();
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
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void AddNewRow(object? sender, RoutedEventArgs e)
    {


    }
}


public class CustomRow : IEnumerable
{
    private Dictionary<string, object> _internalDict = new Dictionary<string, object>();

    public object this[string key]
    {
        get { return _internalDict[key]; }
        set { _internalDict[key] = value; }
    }

    public IEnumerator GetEnumerator()
    {
        return _internalDict.GetEnumerator();
    }

    public void Add(string key, object value)
    {
        _internalDict.Add(key, value);
    }
}
