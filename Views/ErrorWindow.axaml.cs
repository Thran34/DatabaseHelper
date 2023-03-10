using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DatabaseHelper;

public partial class ErrorWindow : Window
{
    public ErrorWindow()
    {
        InitializeComponent();
    }

    private void ExitWindow(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}