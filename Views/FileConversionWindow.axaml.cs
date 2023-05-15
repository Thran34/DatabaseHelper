using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.Views;

namespace DatabaseHelper;

public partial class FileConversionWindow : Window
{
    public FileConversionWindow()
    {
        InitializeComponent();
    }

    private void ShowUserInfo(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MinimizeApp(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ExitApp(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void BrowseFileExplorer(object sender, RoutedEventArgs e)
    {

    }
}