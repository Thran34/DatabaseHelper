using DatabaseHelper.Services;
using ReactiveUI;
using System.ComponentModel;

namespace DatabaseHelper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set
            {
                _isUserLoggedIn = value;
                this.RaiseAndSetIfChanged(ref _isUserLoggedIn, value);
            }
        }

        public static bool LoginUser(string username, string password)
        {
            UserService service = new UserService();
            if (service.LoginUser(username, password))
            {
                return true;
            }

            return false;
        }

        public new event PropertyChangedEventHandler? PropertyChanged;
    }
}