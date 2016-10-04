using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper
{
    /// <summary>
    /// Class defines the exceptions related to the WeatherDataService
    /// </summary>
    public class WeatherDataServiceException : System.Exception
    {
        public WeatherDataServiceException() : base() { }
        public WeatherDataServiceException(string strMessage) : base(strMessage) { }
    }
}
