using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherDataHelper.LINQObjects;

namespace WeatherDataHelper
{
    public class OpenWeatherDataService : IWeatherDataService
    {
        #region Singleton

        private static OpenWeatherDataService instance = null;

        /// <summary>
        /// Default Ctor
        /// </summary>
        private OpenWeatherDataService()
        {
            // Load the cities
            LoadJsonCities();
        }

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static OpenWeatherDataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OpenWeatherDataService();
                }
                return instance;
            }
        }

        #endregion

        #region Data_Members

        List<JsonCity> _lstCities;

        #endregion

        #region Consts

        const string CONFIG_APIURL_KEY = "OpenWeatherMap_APIURL";
        const string CONFIG_APPID_KEY = "OpenWeatherMap_APPID";

        #endregion

        #region Methods

        /// <summary>
        /// Method from the interface, gets weather data according to the sent location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public WeatherData GetWeatherData(Location location)
        {
            WeatherData wdResponse = null;

            // Try to find the requested city matching the wanted location
            JsonCity ctyRequestedCity = _lstCities.Find((currCity) => currCity.name == location.City &&
                                                                  currCity.country == location.Country);

            // Check if city was found
            if (ctyRequestedCity != null)
            {
                // Create the HTTP GET request with the requested location ID
                wdResponse = GetResponse("/weather?id=" + ctyRequestedCity._id + "&units=metric");
            }

            return wdResponse;
        }

        /// <summary>
        /// Send the HTTP-GET response from the service
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private WeatherData GetResponse(string queryString)
        {
            using (var client = new WebClient())
            {
                // Retrive the AppID for the service from the config file
                string strApiKey = ConfigurationManager.AppSettings[CONFIG_APPID_KEY];

                // Retrive the API URL from the config file
                string apiUrl = ConfigurationManager.AppSettings[CONFIG_APIURL_KEY];

                Trace.WriteLine("<HTTP - GET - " + queryString + " >");

                string strUrl;

                if (!string.IsNullOrEmpty(strApiKey))
                    strUrl = apiUrl + queryString + "&APPID=" + strApiKey + "&mode=xml";
                else
                    strUrl = apiUrl + queryString;
                
                // Download the webclient response to our weatherdata request
                string response = client.DownloadString(strUrl);

                try
                {
                    // Parse the response
                    WeatherData wdWeatherResult = ParseXmlResult(response);

                    return wdWeatherResult;
                }
                catch (WeatherDataServiceException ex)
                {
                    throw ex;
                } 
            }
        }

        /// <summary>
        /// Parse the XML result into a WeatherData object
        /// </summary>
        /// <param name="strXMLResult"></param>
        /// <returns></returns>
        private WeatherData ParseXmlResult(string strXMLResult)
        {
            try
            {
                // Init the Xdoc using the received string
                XDocument doc = XDocument.Parse(strXMLResult);

                // Parse using XML to LINQ
                IEnumerable<WeatherData> ieWeatherDatares = from c in doc.Descendants("current")
                                                            select new WeatherData()
                                                            {
                                                                City = (from _cmt in c.Descendants("city")
                                                                        select new City()
                                                                        {
                                                                            Id = _cmt.Attribute("id").Value,
                                                                            Name = _cmt.Attribute("name").Value,
                                                                            Country = _cmt.Element("country").Value,
                                                                            Sun = (from _sun in _cmt.Descendants("sun")
                                                                                   select new Sun()
                                                                                   {
                                                                                       Rise = _sun.Attribute("rise").Value,
                                                                                       Set = _sun.Attribute("set").Value,
                                                                                   }).SingleOrDefault(),
                                                                            Coord = (from _coord in _cmt.Descendants("coord")
                                                                                     select new Coord()
                                                                                     {
                                                                                         Lat = _coord.Attribute("lat").Value,
                                                                                         Lon = _coord.Attribute("lon").Value,
                                                                                     }).SingleOrDefault()
                                                                        }).SingleOrDefault(),
                                                                Temperature = (from _tmp in c.Elements("temperature")
                                                                               select new Temperature()
                                                                               {
                                                                                   Value = _tmp.Attribute("value").Value,
                                                                                   Min = _tmp.Attribute("min").Value,
                                                                                   Max = _tmp.Attribute("max").Value,
                                                                                   Unit = _tmp.Attribute("unit").Value,
                                                                               }).SingleOrDefault(),
                                                                Humidity = (from _hmd in c.Elements("humidity")
                                                                            select new Humidity()
                                                                            {
                                                                                Value = _hmd.Attribute("value").Value,
                                                                                Unit = _hmd.Attribute("unit").Value,
                                                                            }).SingleOrDefault(),
                                                                Pressure = (from _hmd in c.Elements("pressure")
                                                                            select new Pressure()
                                                                            {
                                                                                Value = _hmd.Attribute("value").Value,
                                                                                Unit = _hmd.Attribute("unit").Value,
                                                                            }).SingleOrDefault(),
                                                                Wind = (from _wnd in c.Elements("wind")
                                                                        select new Wind()
                                                                        {
                                                                            Speed = (from _spd in _wnd.Elements("speed")
                                                                                     select new Speed()
                                                                                     {
                                                                                         Name = _spd.Attribute("name").Value,
                                                                                         Value = _spd.Attribute("value").Value
                                                                                     }).SingleOrDefault(),
                                                                            Gusts = (from _gst in _wnd.Elements("gusts")
                                                                                     select new Gusts()
                                                                                     {
                                                                                     }).SingleOrDefault(),
                                                                            Direction = (from _dir in _wnd.Elements("direction")
                                                                                         select new Direction()
                                                                                         {
                                                                                             Value = _dir.Attribute("value").Value,
                                                                                             Code = _dir.Attribute("code").Value,
                                                                                             Name = _dir.Attribute("name").Value
                                                                                         }).SingleOrDefault()
                                                                        }).SingleOrDefault(),
                                                                Clouds = (from _cld in c.Elements("clouds")
                                                                          select new Clouds()
                                                                          {
                                                                              Value = _cld.Attribute("value").Value,
                                                                              Name = _cld.Attribute("name").Value
                                                                          }).SingleOrDefault(),
                                                                Visibility = c.Element("visibility").Value,
                                                                Precipitation = (from _pre in c.Elements("precipitation")
                                                                                 select new Precipitation()
                                                                                 {
                                                                                     Mode = _pre.Attribute("mode").Value
                                                                                 }).SingleOrDefault(),
                                                                Weather = (from _wtr in c.Elements("weather")
                                                                           select new Weather()
                                                                           {
                                                                               Number = _wtr.Attribute("number").Value,
                                                                               Value = _wtr.Attribute("value").Value,
                                                                               Icon = _wtr.Attribute("icon").Value
                                                                           }).SingleOrDefault(),
                                                                Lastupdate = (from _wtr in c.Elements("lastupdate")
                                                                              select new Lastupdate()
                                                                              {
                                                                                  Value = _wtr.Attribute("value").Value

                                                                              }).SingleOrDefault()
                                                            };

                // Return the first parsed result
                return ieWeatherDatares.FirstOrDefault();
            }
            catch
            {
                throw new WeatherDataServiceException("Error parsing result");
            }
        }

        /// <summary>
        /// Maps a city name to its ID from the Json File
        /// </summary>
        public void LoadJsonCities()
        {
            try
            {
                // Init the list of the cities
                _lstCities = new List<JsonCity>();

                // Load the json file into the string
                string strJsonFileContent = File.ReadAllText(@"city.list.json");

                // Deserialize the string into the list of cities
                _lstCities = JsonConvert.DeserializeObject<List<JsonCity>>(strJsonFileContent);
            }
            catch
            {
                throw new WeatherDataServiceException("Error parsing JSON city list");
            }
        }

        #endregion
    }
}
