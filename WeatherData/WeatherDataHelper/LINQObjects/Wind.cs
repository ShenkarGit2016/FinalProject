using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper.LINQObjects
{
    public class Wind
    {
        public Speed Speed { get; set; }
        public Gusts Gusts { get; set; }
        public Direction Direction { get; set; }
    }
}
