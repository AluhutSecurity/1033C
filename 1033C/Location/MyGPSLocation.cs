using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Location {
    /// <summary>
    /// a gps location that can be targeted by the drone pilot 
    /// </summary>
    public class MyGPSLocation {
        public double Altitude { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }
                      

        public MyGPSLocation( double altitude, double latitude, double longitude ) {
            this.Altitude = altitude;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
