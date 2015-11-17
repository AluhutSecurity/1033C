using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    public class MyDrone {
        public ulong UID { get; }

        public DateTime LastUpdate { get; private set; }

        private Delivery.MyPackage package;
        public Delivery.MyPackage Package {
            get {
                return this.package;
            }
            set {
                if(this.package != value ) {
                    this.package = value;
                    this.LastUpdate = DateTime.Now;
                }
            }
        }

        private MyDroneState state;
        public MyDroneState State {
            get { return this.state; }
            set {
                if ( value != this.state ) {
                    LastUpdate = DateTime.Now;
                    this.state = value;
                }
            }
        }

        private Location.MyGPSPosition position;
        public Location.MyGPSPosition Position {
            get {
                return this.position;
            }
            set {
                if ( value != this.position ) {
                    LastUpdate = DateTime.Now;
                    this.position = value;
                }
            }
        }

        public float Capacity => Defines.MAX_PACKAGE_WEIGHT;

        public MyDrone(ulong uid) {
            this.UID = uid;
            this.state = MyDroneState.Online;
            LastUpdate = DateTime.Now;
        }

        public MyDrone( ulong uid, MyDroneState initState ) : this( uid ) {
            this.state = initState;
        }
    }
}
