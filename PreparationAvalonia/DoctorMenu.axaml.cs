using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PreparationAvalonia;

public partial class DoctorMenu : Window
{
    public DoctorMenu()
    {
        InitializeComponent();
    }

    private void ViewUserInfo(object? sender, RoutedEventArgs e)
    {
        ViewUserInfoWindow viewUserInfoWindow = new ViewUserInfoWindow();
        viewUserInfoWindow.Show();
        Close();
    }

    private void MedCart(object? sender, RoutedEventArgs e)
    {
        
    }
}