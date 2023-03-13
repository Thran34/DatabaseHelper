using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
using System;
using System.Threading.Tasks;

namespace DatabaseHelper.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var k = new GetStartedWindow();
            Content = k.Content;
        }

        private void ExitApp(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeApp(object? sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void LoginUser(object? sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Text;
            var mainWindow = new MainWindow();
            var getStartedWindow = new GetStartedWindow();
            bool isValid = await Task.Run(() => MainWindowViewModel.ValidateUserCredentials(username, password));

            if (isValid)
            {
                getStartedWindow.Show();
            }
            else
            {
                var errorWindow = new ErrorWindow();
                errorWindow.Show();
            }
        }


        private void RegisterUser(object? sender, RoutedEventArgs e)
        {
            var registerUserWindow = new RegisterUserWindow();
            registerUserWindow.Show();
        }

        private void LearnDotNet(object? sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://learn.microsoft.com/en-us/dotnet/");
        }

        private async void LearnSql(object? sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16");
        }

        private void OpenFileConverter(object? sender, RoutedEventArgs e)
        {
            var fileConversionWindow = new FileConversionWindow();
            fileConversionWindow.Show();
            Close();
        }
    }
}