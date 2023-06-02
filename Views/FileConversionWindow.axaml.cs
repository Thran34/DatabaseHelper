using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
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

    private async void BrowseFileExplorer(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog();
        string directoryName = await dialog.ShowAsync(this);
        FileConversionViewModel viewModel = new FileConversionViewModel();
        viewModel.CreateDatabase(directoryName);
    }
}
