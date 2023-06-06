using ReactiveUI;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DatabaseHelper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn
        {
            get { return _isUserLoggedIn; }
            set
            {
                _isUserLoggedIn = value;
                this.RaiseAndSetIfChanged(ref _isUserLoggedIn, value);
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}