using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper
{
    public class WeatherDataServiceFactory
    {

        #region Consts

        // Consts identifying the weather services
        public const int OPEN_WEATHER_MAP = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Factory method for returning the wanted WeatherDataService
        /// </summary>
        /// <param name="cChoice"></param>
        /// <returns></returns>
        public static IWeatherDataService GetWeatherDataService(int nOption)
        {
            IWeatherDataService wdsWeatherService = null;

            switch (nOption)
            {
                case OPEN_WEATHER_MAP:
                    wdsWeatherService = OpenWeatherDataService.Instance;
                    break;
                default:
                    wdsWeatherService = null;
                    break;
            }

            return wdsWeatherService;

        } 

        #endregion
    }
}
