using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DatabaseHelper;

public partial class InfoWindow : Window
{
    private string _message;
    public InfoWindow()
    {

    }
    public InfoWindow(string message)
    {
        _message = message;
        InitializeComponent();
        Message = this.FindControl<TextBlock>("Message");
        Message.Text = _message;
    }

    private void ExitWindow(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}