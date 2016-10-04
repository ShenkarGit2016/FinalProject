using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper.LINQObjects
{
    public class JsonCoord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class JsonCity
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public JsonCoord coord { get; set; }
    }
}
