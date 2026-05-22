using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WegaisApp.Core.WeatherAPIIntegration
{
    public class WeatherResponse
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string? timezone { get; set; }
        public string? timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public Current_Units? current_units { get; set; }
        public Current? current { get; set; }

        public string CurrentTemperature => $"{current.temperature_2m} {current_units.temperature_2m}";
        public string Info => $"Согласно open-meteo.com на {DateTime.Parse(current.time).ToString("g")}" +
            $"\n({latitude}:{longitude}) {timezone}";
    }

    public class Current_Units
    {
        public string? time { get; set; }
        public string? interval { get; set; }
        public string? temperature_2m { get; set; }
        public string? weather_code { get; set; }
    }

    public class Current
    {
        public string? time { get; set; }
        public int? interval { get; set; }
        public float? temperature_2m { get; set; }
        public int? weather_code { get; set; }
    }

}
