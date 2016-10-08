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
            IWeatherDataService iwdsWeatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);
            WeatherData wdTestWeatherData = iwdsWeatherService.GetWeatherData(new Location("Tel Aviv", "IL"));

            Console.WriteLine("City: " + wdTestWeatherData.City.Name + ", " + wdTestWeatherData.City.Country);
            Console.WriteLine("Temperature: " + wdTestWeatherData.Temperature.Value + "°C");
            Console.WriteLine("Humidity: " + wdTestWeatherData.Humidity.Value + wdTestWeatherData.Humidity.Unit);
            Console.WriteLine("Weather: " + wdTestWeatherData.Weather.Value);
            Console.WriteLine("Wind: " + wdTestWeatherData.Wind.Direction.Name + " " + wdTestWeatherData.Wind.Speed.Name);
            Console.WriteLine("Clouds: " + wdTestWeatherData.Clouds.Name);

            Console.ReadKey();
        }
    }
}
