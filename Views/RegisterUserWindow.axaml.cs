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
            Close();
        }
    }
}