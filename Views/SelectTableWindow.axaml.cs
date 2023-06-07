using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.ViewModels;
using System;

namespace DatabaseHelper;

public partial class SelectTableWindow : Window
{
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
        tables = this.FindControl<ListBox>("tables");
        var viewModel = new SelectTableViewModel();
        viewModel.PopulateList(selectedDatabase, tables);
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

    private void SelectTable(object? sender, RoutedEventArgs e)
    {
        if (tables.SelectedItem != null)
        {
            string? selectedTable = tables.SelectedItem.ToString();
            if (selectedTable != null)
            {
                SelectWholeTableWindow selectTableWindow = new SelectWholeTableWindow(selectedTable, _selectedDatabase);
                selectTableWindow.Show();
            }

            this.Close();
        }
    }
}