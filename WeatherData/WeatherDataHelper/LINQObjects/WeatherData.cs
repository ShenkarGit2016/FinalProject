using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper.LINQObjects
{
        public class WeatherData
        {
            public City City { get; set; }
            public Temperature Temperature { get; set; }
            public Humidity Humidity { get; set; }
            public Pressure Pressure { get; set; }
            public Wind Wind { get; set; }
            public Clouds Clouds { get; set; }
            public string Visibility { get; set; }
            public Precipitation Precipitation { get; set; }
            public Weather Weather { get; set; }
            public Lastupdate Lastupdate { get; set; }
        }
}
