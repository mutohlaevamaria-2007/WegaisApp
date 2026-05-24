using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WegaisApp.WPF
{
    public class WeatherCodeToIconPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = value switch
            {
                0 => "clear",
                1 or 2 => "partly-cloudy",
                3 => "cloudy",
                45 or 48 => "fog",
                51 or 53 or 55 or 61 or 63 or 65 => "rain",
                56 or 57 or 66 or 67 or 85 or 86 => "freezing-rain",
                71 or 73 or 75 or 77 => "snow",
                80 or 81 or 82 => "heavy-rain",
                >= 95 => "thunderstorm",
                _ => "unknow"
            };

            return $"pack://application:,,,/WegaisApp.WPF;component/WeatherIcons/{name}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
