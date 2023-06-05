using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
using DatabaseHelper.Views;
using System.Linq;

namespace DatabaseHelper;

public partial class FileConversionWindow : Window
{
    private string selectedDirectory;
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
        selectedDirectory = await dialog.ShowAsync(this);
        FilePathTextBox.Text = selectedDirectory.Split(@"\").Last();
    }

    private async void Submit(object sender, RoutedEventArgs e)
    {
        var viewModel = new FileConversionViewModel();
        viewModel.CreateDatabase(selectedDirectory);
    }
}
