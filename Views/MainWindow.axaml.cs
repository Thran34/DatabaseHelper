using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DatabaseHelper.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var k = new GetStartedWindow();
            k.Show();
        }
    }
}