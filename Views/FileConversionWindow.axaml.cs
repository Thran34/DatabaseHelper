using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
using DatabaseHelper.Views;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;

namespace DatabaseHelper;

public partial class FileConversionWindow : Window
{
    private string _selectedDirectory;
    private string _selectedDatabase;

    public FileConversionWindow()
    {

    }
    public FileConversionWindow(string selectedDatabase)
    {
        _selectedDatabase = selectedDatabase;
        InitializeComponent();
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

    private async void BrowseFileExplorer(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog();
        _selectedDirectory = (await dialog.ShowAsync(this))!;
        if (!_selectedDirectory.IsNullOrEmpty())
        {
            FilePathTextBox.Text = _selectedDirectory!.Split(@"\").Last();
        }
    }

    private void Submit(object sender, RoutedEventArgs e)
    {
        var viewModel = new FileConversionViewModel();
        viewModel.CreateDatabase(_selectedDirectory, _selectedDatabase);
    }
}
