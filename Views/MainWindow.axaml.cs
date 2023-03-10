using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
using System;

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

        private void LoginUser(object? sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Text;
            var mainWindow = new MainWindow();
            var getStartedWindow = new GetStartedWindow();
            bool isValid = MainWindowViewModel.ValidateUserCredentials(username, password);

            if (isValid)
            {
                getStartedWindow.Show();
            }
        }

        private void RegisterUser(object? sender, RoutedEventArgs e)
        {
            var registerUserWindow = new RegisterUserWindow();
            registerUserWindow.Show();
        }
    }
}