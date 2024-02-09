using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using PreparationAvalonia.Models;

namespace PreparationAvalonia;

public partial class ViewUserInfoWindow : Window
{
    public ViewUserInfoWindow()
    {
        InitializeComponent();
        PatientsWindow();
    }

    private async Task PatientsWindow()
    {
        List<PatientsFioModel> usersFio;
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5263/api/Patient/GetPatient");
            string content = await responseMessage.Content.ReadAsStringAsync();
            usersFio = JsonConvert.DeserializeObject<List<PatientsFioModel>>(content)!.ToList();
        }
        ComboBoxPatients.ItemsSource = usersFio.Select(x => new
        {
            NamePatients = x.Fio
        }).ToList();
        if (ComboBoxPatients != null)
        {
            string fioUser = usersFio[ComboBoxPatients.SelectedIndex].Fio!;
            
        }
        
    }
}