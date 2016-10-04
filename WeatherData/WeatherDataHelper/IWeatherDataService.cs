using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataHelper.LINQObjects;

namespace WeatherDataHelper
{
    public interface IWeatherDataService
    {
        /// <summary>
        /// Method from the interface, gets weather data according to the sent location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        WeatherData GetWeatherData(Location location);
    }
}
