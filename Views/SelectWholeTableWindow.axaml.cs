using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.ViewModels;
using System;

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
        var viewModel = new SelectWholeTableViewModel(selectedTable, selectedDatabase, dataGrid);
        viewModel.PopulateDataTable();
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
