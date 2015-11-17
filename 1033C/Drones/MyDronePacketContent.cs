using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    public enum MyDronePacketContent : ushort {
        SignalHalt,
        SignalBye,
        PackagePickup,
        Invalid,
        StatusUpdate,
        LocationUpdate
    }
}
