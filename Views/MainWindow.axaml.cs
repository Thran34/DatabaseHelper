using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DatabaseHelper.Views
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isUserLoggedIn;
        public MainWindow(bool isUserLoggedIn)
        {
            InitializeComponent();
            _isUserLoggedIn = isUserLoggedIn;

            if (_isUserLoggedIn)
            {
                LoginPanel.IsVisible = false;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectDatabase(object? sender, RoutedEventArgs e)
        {
            var selectDatabaseWindow = new SelectDatabaseWindow(false);
            selectDatabaseWindow.Show();
            this.Close();
        }

        private void SelectDatabaseForDataConversion(object? sender, RoutedEventArgs e)
        {
            var selectDatabaseWindow = new SelectDatabaseWindow(true);
            selectDatabaseWindow.Show();
            this.Close();
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
            bool isValid = await Task.Run(() => MainWindowViewModel.LoginUser(username, password));

            if (!isValid)
            {
                var errorWindow = new ErrorWindow("Invalid user name or password.");
                errorWindow.Show();
            }
            else
            {
                EnableControlButtons();
                DisableLoginPanel();
            }
        }

        private void DisableLoginPanel()
        {
            LoginPanel.IsVisible = false;
            LoggedInStatus.IsVisible = true;
            LoggedInStatus.Content = "Welcome! You are logged in.";
        }

        private void EnableControlButtons()
        {
            this.Draw.IsEnabled = true;
            this.DataImport.IsEnabled = true;
            this.ShowDatabase.IsEnabled = true;
            _isUserLoggedIn = true;
        }

        private void RegisterUser(object? sender, RoutedEventArgs e)
        {
            var registerUserWindow = new RegisterUserWindow();
            registerUserWindow.Show();
        }

        private void LearnDotNet(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://learn.microsoft.com/en-us/dotnet/",
                UseShellExecute = true
            });
        }

        private void LearnSql(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16/",
                UseShellExecute = true
            });
        }

        private void OpenDrawIo(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://app.diagrams.net/",
                UseShellExecute = true
            });
        }
    }
}