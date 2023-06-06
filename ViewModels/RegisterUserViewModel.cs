using DatabaseHelper.Services;

namespace DatabaseHelper.ViewModels
{
    internal class RegisterUserViewModel
    {
        public bool RegisterUser(string username, string password)
        {
            UserService service = new UserService();
            if (service.RegisterUser(username, password))
            {
                return true;
            }

            return false;
        }
    }
}
