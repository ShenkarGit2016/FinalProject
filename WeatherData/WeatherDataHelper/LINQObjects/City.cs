using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper.LINQObjects
{
    public class City
    {
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public Sun Sun { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
