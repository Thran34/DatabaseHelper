using Avalonia.Controls;
using Avalonia.Interactivity;
using DatabaseHelper.ViewModels;
using DatabaseHelper.Views;

namespace DatabaseHelper;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }
    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Text;
        MainWindow mainWindow = new MainWindow();
        var getStartedWindow = new GetStartedWindow();
        // Validate the user's credentials
        bool isValid = LoginViewModel.ValidateUserCredentials(username, password);

        if (isValid)
        {
            getStartedWindow.Show();
            Close();
        }
        else
        {
            mainWindow.Show();
            Close();
        }
    }
}