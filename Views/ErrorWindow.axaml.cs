using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DatabaseHelper;

public partial class ErrorWindow : Window
{
    public ErrorWindow()
    {
        InitializeComponent();
    }

    private void Close(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}