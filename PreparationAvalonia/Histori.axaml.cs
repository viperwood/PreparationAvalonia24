using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;

namespace PreparationAvalonia;

public partial class Histori : Window
{
    public Histori()
    {
        InitializeComponent();
        HttpHistori();
    }


    private async Task HttpHistori()
    {
        HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync("http://localhost:5263/api/Random/GetRandom");
        string content = await responseMessage.Content.ReadAsStringAsync();
        List<Rand> info = JsonConvert.DeserializeObject<List<Rand>>(content)!.ToList();
        HistorInfo.ItemsSource = info.ToList();
    }
}
public class Rand
{
    public int PersonCode { get; set; }
    public string? PersonRole { get; set; }
    public int LastSecurityPointNumber { get; set; }
    public string? LastSecurityPointDirection { get; set; }
    public DateTime LastSecurityPointTime { get; set; }
}
