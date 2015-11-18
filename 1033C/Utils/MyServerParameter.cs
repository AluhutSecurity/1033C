using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Utils {
    public class MyServerParameter {
        public int Port { get; set; }
        public System.Net.IPAddress LocalIP { get; set; }

        public MyServerParameter() {
            this.LocalIP = System.Net.IPAddress.Any;
        }

        public MyServerParameter( System.Net.IPAddress addr, int port ) {
            this.LocalIP = addr;
            this.Port = port;
        }
    }
}
