using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C {
    /// <summary>
    /// stuff your defines here so they are all together
    /// </summary>
    public static class Defines {
        /// <summary>
        /// defines the maximum supported package weight
        /// this can(should) be stored with a specific drone,
        /// but as only one drone will be used this is sufficient
        /// </summary>
        public static float MAX_PACKAGE_WEIGHT = 2000;

        public static int DEFAULT_DRONESERVER_LOCALPORT = 60123;

        public static System.Net.IPAddress DEFAULT_DRONESERVER_LISTENERIP = System.Net.IPAddress.Any;

        public static System.Net.IPAddress DEFAULT_DRONESERVER_IP = System.Net.IPAddress.Parse( "127.0.0.1" );
    }
}
