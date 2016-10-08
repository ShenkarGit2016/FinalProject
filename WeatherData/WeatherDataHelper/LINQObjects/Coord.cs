using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataHelper.LINQObjects
{
    public class Coord
    {
        public string Lon { get; set; }
        public string Lat { get; set; }

        public override bool Equals(object obj)
        {
            Coord CoordObj = obj as Coord;

            if (CoordObj == null)
                return false;
            else
                return Lon.Equals(CoordObj.Lon) &&
                       Lat.Equals(CoordObj.Lat);
        }

    }
}
