using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WegaisApp.Core.WeatherAPIIntegration
{
    public class WeatherClient
    {
        private static Dictionary<string, Coords> _locations = new()
        {
            { "Ufa" , new Coords(54.7431f, 55.9678f)},
            { "hellish-cold", new Coords(-90f, -90f)} // FIX: тестовый случай
        };

        public async Task<WeatherResponse> GetCurrentWeatherAsync(string locationName = "Ufa")
        {
            Coords location = _locations[locationName];
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={location.latitude.ToString(CultureInfo.InvariantCulture)}&longitude={location.longitude.ToString(CultureInfo.InvariantCulture)}&current=temperature_2m,weather_code&timezone=auto";
            using HttpClient client = new HttpClient();
            return await client.GetFromJsonAsync<WeatherResponse>(url);
        }

        public record struct Coords(float latitude, float longitude);
    }
}