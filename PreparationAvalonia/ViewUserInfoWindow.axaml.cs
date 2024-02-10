using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
    private List<PatientsFioModel> usersFio; 
    private async Task PatientsWindow()
    {
        HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Patient/GetPatient");
        string content = await responseMessage.Content.ReadAsStringAsync();
        usersFio = JsonConvert.DeserializeObject<List<PatientsFioModel>>(content)!.ToList();
        
        ComboBoxPatients.ItemsSource = usersFio.Select(x => new
        {
            NamePatients = x.Fio
        }).ToList();
    }

    private async void SerchButton(object? sender, RoutedEventArgs e)
    {
        if (ComboBoxPatients != null)
        {
            HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync($"http://localhost:5263/api/FullInfoPatient/GetFullInfoPatient?name={usersFio[ComboBoxPatients.SelectedIndex].Fio}");
            string content = await responseMessage.Content.ReadAsStringAsync();
            List<PatientFullInfoModels> fullInfo =
                JsonConvert.DeserializeObject<List<PatientFullInfoModels>>(content)!.ToList();
            if (fullInfo[0].Email != null)
            {
                PanelInfo.IsVisible = true;
                EmailBlock.Text = fullInfo[0].Email;
                PhoneBlock.Text = fullInfo[0].Phone;
                AddressBlock.Text = fullInfo[0].Address;
                GendernameBlock.Text = fullInfo[0].Gendername;
                BirthdayBlock.Text = fullInfo[0].Birthday.ToString();
                SeriespBlock.Text = fullInfo[0].Seriesp.ToString();
                NumberpBlock.Text = fullInfo[0].Numberp.ToString();
                FioBlock.Text = fullInfo[0].Fio;
                InsurancecompanynameBlock.Text = fullInfo[0].Insurancecompanyname;
                PlaceofworknameBlock.Text = fullInfo[0].Placeofworkname;
                PolicyvalidityBlock.Text = fullInfo[0].Policyvalidity.ToString();
                NumberpolisBlock.Text = fullInfo[0].Numberpolis;
                DataexpirationinsurancepolisyBlock.Text = fullInfo[0].Dataexpirationinsurancepolisy.ToString();
                DataissuemcBlock.Text = fullInfo[0].Dataissuemc.ToString();
            }
        }
        
    }
}