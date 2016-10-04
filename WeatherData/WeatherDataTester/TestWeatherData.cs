using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherDataHelper;

namespace WeatherDataTester
{
    [TestClass]
    public class TestWeatherData
    {
        [TestMethod]
        public void TestGetWeatherDataOne()
        {
            IWeatherDataService wdsWeatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);
        }
    }
}
