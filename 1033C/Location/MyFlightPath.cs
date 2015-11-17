using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Location {
    /// <summary>
    /// represents the flightpath that is to take by the 
    /// delivery entity to the recipient
    /// 
    /// its basicly just an enumeration of waypoints
    /// </summary>
    public class MyFlightPath {
        private List<MyGPSLocation> path;

        public IEnumerable<MyGPSLocation> Waypoints => this.path.AsEnumerable();

        public MyFlightPath() {
            this.path = new List<MyGPSLocation>();
        }

        public MyFlightPath(IEnumerable<MyGPSLocation> waypoints) : this(){
            this.path.AddRange( waypoints );
        }
    }
}
