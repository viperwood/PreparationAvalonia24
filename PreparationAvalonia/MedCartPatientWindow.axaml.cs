using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using PreparationAvalonia.Models;

namespace PreparationAvalonia;

public partial class MedCartPatientWindow : Window
{
    public MedCartPatientWindow()
    {
        InitializeComponent();
        DiagnosisBox();
    }

    private List<string> _preparatList = new();
    private List<ReseptModel> _resept = new();

    private async Task DiagnosisBox()
    {
        HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Diagnosis/GetDiagnosis");
        string content = await responseMessage.Content.ReadAsStringAsync();
        List<string> diagnosList = JsonConvert.DeserializeObject<List<string>>(content)!.ToList();
        DiagnosComboBox.ItemsSource = diagnosList.Select(x => new
        {
            DiagnosName = x
        }).ToList();
        
        HttpResponseMessage responseMes = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Priparation/GetPriparation");
        string contents = await responseMes.Content.ReadAsStringAsync();
        _preparatList = JsonConvert.DeserializeObject<List<string>>(contents)!.ToList();
        PreparationComboBox.ItemsSource = _preparatList.Select(x => new
        {
            PreparationName = x
        }).ToList();
    }

    private void AddListPreparat(object? sender, RoutedEventArgs e)
    {
        if (_resept.Count < 10)
        {
            ReseptModel reseptModel = new ReseptModel();
            reseptModel.Dosa = DosaBox.Text;
            reseptModel.Format = FormaBox.Text;
            reseptModel.NamePreparation = _preparatList[PreparationComboBox.SelectedIndex];
            _resept.Add(reseptModel);
            ReseptList.ItemsSource = _resept.ToList();   
        }
    }
}