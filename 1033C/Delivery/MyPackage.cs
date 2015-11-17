using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Delivery {

    /// <summary>
    /// represents a package that is to be delivered
    /// uid, recipient and weight are mandatory
    /// 
    /// to receive a MyPackage instance query the MyPackageManager
    /// </summary>
    public class MyPackage {
        public ulong UID { get; }

        public MyAddress Recipient { get; }

        public MyAddress Sender { get; }

        public float Weight { get; }

        public MyPackageState State { get; internal set; }

        public Location.MyFlightPath FlightPath { get; internal set; }


        public static bool operator==(MyPackage a, MyPackage b) {
            return a.UID == b.UID;
        }

        public static bool operator!=(MyPackage a, MyPackage b) {
            return a.UID != b.UID;
        }

        internal MyPackage( ulong uid ) {
            this.UID = uid;
            this.State = MyPackageState.Registered;
            this.FlightPath = null;
        }

        internal MyPackage( ulong uid, MyAddress recipient, float weight ) : this( uid ) {
            this.Recipient = recipient;
            this.Weight = weight;
        }

        internal MyPackage( ulong uid, MyAddress recipient, MyAddress sender, float weight ) : this( uid, recipient, weight ) {
            this.Sender = sender;
        }
    }
}
