using DatabaseHelper.Services;
using Microsoft.IdentityModel.Tokens;

namespace DatabaseHelper.ViewModels
{
    internal class RegisterUserViewModel
    {
        private ErrorWindow? _errorWindow;
        public bool RegisterUser(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                _errorWindow = new ErrorWindow("Missing username or password.");
                _errorWindow.Show();
                return false;
            }
            UserService service = new UserService();
            if (service.RegisterUser(username, password))
            {
                return true;
            }

            _errorWindow = new ErrorWindow("User already exists.");
            _errorWindow.Show();
            return false;
        }
    }
}
