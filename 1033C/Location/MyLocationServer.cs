using System;
using _1033C.Delivery;

namespace _1033C.Location {
    /// <summary>
    /// represents the connection to the location server
    /// can be queried to calculate / verify a flightpath to a given recipient
    /// </summary>
    public class MyLocationServer {
        internal MyFlightPath CalculateFlightPath( MyAddress recipient, float weight ) {
            throw new NotImplementedException();
        }
    }
}