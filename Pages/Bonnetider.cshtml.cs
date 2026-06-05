using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WebApplication1.Pages;

public class Bonnetider : PageModel
{
    public string Fajr { get; set; } = "";
    public string Shuruk { get; set; } = "";
    public string Dhuhr { get; set; } = "";
    public string Asr { get; set; } = "";
    public string Maghrib { get; set; } = "";
    public string Isha { get; set; } = "";

    public async Task OnGetAsync()
    {
        var client = new HttpClient();
        var today = DateTime.Now;
        var url = $"https://api.aladhan.com/v1/timings/{today.Day}-{today.Month}-{today.Year}?latitude=55.3959&longitude=10.3883&method=3";

        var response = await client.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        var timings = json.RootElement
            .GetProperty("data")
            .GetProperty("timings");

        Fajr = timings.GetProperty("Fajr").GetString() ?? "";
        Shuruk = timings.GetProperty("Sunrise").GetString() ?? "";
        Dhuhr = timings.GetProperty("Dhuhr").GetString() ?? "";
        Asr = timings.GetProperty("Asr").GetString() ?? "";
        Maghrib = timings.GetProperty("Maghrib").GetString() ?? "";
        Isha = timings.GetProperty("Isha").GetString() ?? "";
    }
}