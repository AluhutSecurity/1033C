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
        private List<MyGPSPosition> path;

        public IEnumerable<MyGPSPosition> Waypoints => this.path.AsEnumerable();

        public MyFlightPath() {
            this.path = new List<MyGPSPosition>();
        }

        public MyFlightPath(IEnumerable<MyGPSPosition> waypoints) : this(){
            this.path.AddRange( waypoints );
        }
    }
}
