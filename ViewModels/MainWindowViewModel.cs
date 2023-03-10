using System.Linq;

namespace DatabaseHelper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static bool ValidateUserCredentials(string username, string password)
        {
            using var context = new Context.Context();
            return Enumerable.Any(context.Users, user => username == user.Login && password == user.Password);
        }
    }
}