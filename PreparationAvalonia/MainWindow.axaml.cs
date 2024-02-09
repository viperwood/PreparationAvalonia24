using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using PreparationAvalonia.Models;

namespace PreparationAvalonia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private async void LoginUser(object? sender, RoutedEventArgs routedEventArgs)
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5263/api/Login/GetLogin?loginUser={LoginBox.Text}");
            string content = await responseMessage.Content.ReadAsStringAsync();
            List<LoginUserModel> userInfo = JsonConvert.DeserializeObject<List<LoginUserModel>>(content)!.ToList();
            if (userInfo.Count == 1)
            {
                DoctorMenu doctorMenu = new DoctorMenu();
                doctorMenu.Show();
                Close();
            }
        }
    }
}