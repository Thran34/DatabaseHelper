using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;

namespace DatabaseHelper;

public partial class RegisterUserWindow : Window
{
    public RegisterUserWindow()
    {
        InitializeComponent();
    }

    private void RegisterUser(object? sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Text;
        var registerUserViewModel = new RegisterUserViewModel();
        bool registrationSuccess = registerUserViewModel.RegisterUser(username, password);

        if (registrationSuccess)
        {
            this.IsRegistrationSuccessful.IsVisible = true;
            this.IsRegistrationSuccessful.Content = "User succesfully registered!";
            this.ExitButton.IsVisible = true;
            this.UserPanel.IsVisible = false;

        }
        else
        {
            var errorWindow = new ErrorWindow("User already exists.");
            errorWindow.Show();
        }
    }

    private void ExitWindow(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MinimizeWindow(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}