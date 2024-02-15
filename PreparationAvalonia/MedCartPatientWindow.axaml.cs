using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

    private List<Patients> patients;
    private List<string> _preparatList = new();
    private List<DiagnosisList> diagnosList;
    private List<ReseptModel> _resept = new();
    private List<TipeEventList> tipeEventLists;

    private async Task DiagnosisBox()
    {
        HttpResponseMessage httpResponseMessage = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Patient/GetPatient");
        string cont = await httpResponseMessage.Content.ReadAsStringAsync();
        patients = JsonConvert.DeserializeObject<List<Patients>>(cont)!.ToList();
        PatientsBox.ItemsSource = patients.Select(x => new
        {
            FioPatient = x.Fio
        }).ToList();
        
        HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Diagnosis/GetDiagnosis");
        string content = await responseMessage.Content.ReadAsStringAsync();
        diagnosList = JsonConvert.DeserializeObject<List<DiagnosisList>>(content)!.ToList();
        DiagnosComboBox.ItemsSource = diagnosList.Select(x => new
        {
            DiagnosName = x.Diagnosisname
        }).ToList();
        
        HttpResponseMessage responseMes = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Priparation/GetPriparation");
        string contents = await responseMes.Content.ReadAsStringAsync();
        _preparatList = JsonConvert.DeserializeObject<List<string>>(contents)!.ToList();
        PreparationComboBox.ItemsSource = _preparatList.Select(x => new
        {
            PreparationName = x
        }).ToList();

        HttpResponseMessage message = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/TipeEvent/GetTipeEvent");
        string contantTipe = await message.Content.ReadAsStringAsync();
        tipeEventLists = JsonConvert.DeserializeObject<List<TipeEventList>>(contantTipe)!.ToList();
        TipeDirection.ItemsSource = tipeEventLists.Select(x => new
        {
            NameTipe = x.Eventname
        }).ToList();
    }

    private void AddListPreparat(object? sender, RoutedEventArgs e)
    {
        if (_resept.Count < 10)
        {
            ReseptModel reseptModel = new ReseptModel();
            reseptModel.Dosa = DosaBox.Text;
            reseptModel.Format = FormaBox.Text;
            reseptModel.Recomendation = Recomend.Text;
            reseptModel.NamePreparation = _preparatList[PreparationComboBox.SelectedIndex];
            _resept.Add(reseptModel);
            ReseptList.ItemsSource = _resept.ToList();   
        }
    }

    private async void Next(object? sender, RoutedEventArgs e)
    {
        AddPostDirection addPostDirection = new AddPostDirection();
        addPostDirection.Fio = patients[PatientsBox.SelectedIndex].Userid;
        addPostDirection.Diagnos = diagnosList[DiagnosComboBox.SelectedIndex].Diagnosisid;
        addPostDirection.Anamnis = Anamn.Text;
        addPostDirection.Symptomatic = Simptomat.Text;
        addPostDirection.NameDiagnostic = DiagnosticnameBox.Text;
        addPostDirection.DoctorId = UserLog.UserLogSistem[0];
        addPostDirection.Description = DescriptioneBox.Text;
        addPostDirection.TipeEvent = tipeEventLists[TipeDirection.SelectedIndex].Eventid;
        ReseptModel reseptModel = new ReseptModel();
        reseptModel.Recomendation = Recomend.Text;
        HttpResponseMessage message = await HttpClientHelper.Client.PostAsJsonAsync("http://localhost:5263/api/AddDirection/PostDirection",addPostDirection);
        HttpResponseMessage resp = await HttpClientHelper.Client.PostAsJsonAsync("http://localhost:5263/api/AddListPreparation/PostListPriporation",_resept);
    }
}

class Patients
{
    public string? Fio { get; set; }
    public int Userid { get; set; }
}

class DiagnosisList
{
    public string? Diagnosisname { get; set; }
    public int Diagnosisid { get; set; }
}

class TipeEventList
{
    public string? Eventname { get; set; }
    public int Eventid { get; set; }
}

class AddPostDirection
{
    public int? Fio { get; set; }
    public int? Diagnos { get; set; }
    public string? Anamnis { get; set; }
    public string? Symptomatic { get; set; }
    public string? NameDiagnostic { get; set; }
    public string? Description { get; set; }
    public int TipeEvent { get; set; } 
    public int DoctorId { get; set; } 
}