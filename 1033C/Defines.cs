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


        //http://www.ncftp.com/ncftpd/doc/misc/ephemeral_ports.html#Windows:
        //As of Windows Vista and Windows Server 2008, Windows now uses a large range (49152-65535) by default,
        //according to Microsoft Knowledgebase Article 929851 (https://support.microsoft.com/en-us/kb/929851). 
        //That same article also shows how you can change the range if desired, 
        //but the default range is now sufficient for most servers.
        public static ushort HIGHESTVALIDPORT = 65535;
        public static ushort LOWESTVALIDPORT = 49152;
    }
}
