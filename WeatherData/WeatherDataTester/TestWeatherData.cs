using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherDataHelper;
using WeatherDataHelper.LINQObjects;

namespace WeatherDataTester
{
    /*
     * Tests for this library are inefficient because the information is received from a web service,
     * the data is unpredictable, we could not guess the accurate weather of the target location
     * testing for this class should be dynamic, seen by the eyes of the user and not statically choosen
     */
    [TestClass]
    public class TestWeatherData
    {
        /// <summary>
        /// Checking if weather data with an invalid location returns the expected value
        /// </summary>
        [TestMethod]
        public void CheckGetWeatherDataInvalidLocation()
        {
            IWeatherDataService wdsWeatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);

            WeatherData result = wdsWeatherService.GetWeatherData(new Location("Testing", "Testing"));

            // Invalid location query should return null
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Same as above
        /// Because the data from the service is dynamic and we can't predict it in the UnitTest(degrees,wind,humidity...)
        /// what we could check is the static information about the location we query.
        /// </summary>
        [TestMethod]
        public void CheckTelAvivWeatherData()
        {
            IWeatherDataService wdsWeatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);

            WeatherData result = wdsWeatherService.GetWeatherData(new Location("Tel Aviv", "IL"));
            Coord coorExpectedCoord = new Coord() { Lat = "32.08", Lon = "34.78" };

            Assert.AreEqual(coorExpectedCoord, result.City.Coord);
            Assert.AreEqual("293397", result.City.Id);
            Assert.AreEqual("metric", result.Temperature.Unit);
        }

        /// <summary>
        /// Because the data from the service is dynamic and we can't predict it in the UnitTest(degrees,wind,humidity...)
        /// what we could check is the static information about the location we query.
        /// </summary>
        [TestMethod]
        public void CheckMadridWeatherData()
        {
            IWeatherDataService wdsWeatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);

            WeatherData result = wdsWeatherService.GetWeatherData(new Location("Madrid", "ES"));
            Coord coorExpectedCoord = new Coord() { Lat = "40.49", Lon = "-3.68" };

            Assert.AreEqual(coorExpectedCoord, result.City.Coord);
            Assert.AreEqual("6359304", result.City.Id);
            Assert.AreEqual("metric", result.Temperature.Unit);
        }
    }
}
