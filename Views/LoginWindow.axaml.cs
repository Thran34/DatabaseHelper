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

        // Validate the user's credentials
        bool isValid = LoginViewModel.ValidateUserCredentials(username, password);

        if (isValid)
        {
            // Create a session for the user
            //UserSession.CreateSession(username);

            // Navigate to the main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        else
        {

        }
    }
}