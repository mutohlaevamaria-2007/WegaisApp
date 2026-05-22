using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WegaisApp.Core.WeatherAPIIntegration
{
    public class WeatherClient
    {
        private static string url = "https://api.open-meteo.com/v1/forecast?latitude=54.7431&longitude=55.9678&current=temperature_2m,weather_code&timezone=auto";

        public async Task<WeatherResponse> GetCurrentWeatherAsync()
        {
            using HttpClient client = new HttpClient();
            return await client.GetFromJsonAsync<WeatherResponse>(url);
        }
    }
}