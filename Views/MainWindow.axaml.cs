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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void SelectDatabase(object? sender, RoutedEventArgs e)
        {
            var k = new SelectDatabaseWindow();
            k.Show();
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
            bool isValid = await Task.Run(() => MainWindowViewModel.ValidateUserCredentials(username, password));

            if (!isValid)
            {
                var errorWindow = new ErrorWindow();
                errorWindow.Show();
            }
            else
            {
                EnableControlButtons();

            }
        }

        private void EnableControlButtons()
        {
            this.Draw.IsEnabled = true;
            this.DataImport.IsEnabled = true;
            this.ShowDatabase.IsEnabled = true;
            this.Disk.IsEnabled = true;
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


        private async void LearnSql(object? sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16/",
                UseShellExecute = true
            });
        }

        private void OpenFileConverter(object? sender, RoutedEventArgs e)
        {
            var fileConversionWindow = new FileConversionWindow();
            fileConversionWindow.Show();
            Close();
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