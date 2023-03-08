using Avalonia.Controls;
using Avalonia.Interactivity;
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

        private void Button_OnClick_Login(object? sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void ExitApp(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeApp(object? sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}