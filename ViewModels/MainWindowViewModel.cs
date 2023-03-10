namespace DatabaseHelper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static bool ValidateUserCredentials(string username, string password)
        {
            using (var context = new Context.Context())
            {
                foreach (var user in context.Users)
                {
                    if (username == user.Login && password == user.Password)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}