using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    public enum MyDroneState {
        AwaitingLanding,
        AwaitingTakeoff,
        Evading,
        Leaving,
        Offline,
        Online,
        OnTour,
        PackagePickup,
        Ready,
        Unknown
    }
}
