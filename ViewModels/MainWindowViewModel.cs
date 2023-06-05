using System.ComponentModel;
using System.Linq;

namespace DatabaseHelper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private bool _isUserLoggedIn;
        public bool isUserLoggedIn
        {
            get { return _isUserLoggedIn; }
            set
            {
                _isUserLoggedIn = value;
                OnPropertyChanged(nameof(isUserLoggedIn));
            }
        }
        public static bool ValidateUserCredentials(string username, string password)
        {
            using var context = new Context.Context();
            bool isValid = Enumerable.Any(context.Users, user => username == user.Login && password == user.Password);

            if (isValid)
            {
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}