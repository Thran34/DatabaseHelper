using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DatabaseHelper.ViewModels;
using DatabaseHelper.Views;
using System;

namespace DatabaseHelper;

public partial class SelectDatabaseWindow : Window
{
    private readonly bool _shouldOpenFileConversion;

    public SelectDatabaseWindow()
    {
        InitializeComponent();
        PopulateList();
    }
    public SelectDatabaseWindow(bool shouldOpenFileConversion)
    {
        _shouldOpenFileConversion = shouldOpenFileConversion;
        InitializeComponent();
        PopulateList();
    }

    private void PopulateList()
    {
        Databases = this.FindControl<ListBox>("Databases");
        var viewModel = new SelectDatabaseViewModel();
        viewModel.PopulateList(Databases);
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

    private void SelectDatabase(object? sender, RoutedEventArgs e)
    {
        var viewModel = new SelectDatabaseViewModel();
        viewModel.SelectDatabase(Databases, _shouldOpenFileConversion);
        this.Close();
    }
}