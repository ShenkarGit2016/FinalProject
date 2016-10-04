using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataHelper;
using WeatherDataHelper.LINQObjects;

namespace ApiUser
{
    class Program
    {
        static void Main(string[] args)
        {
            IWeatherDataService tmp = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);
            WeatherData wdTest = tmp.GetWeatherData(new Location("Tel Aviv", "IL"));
        }
    }
}
