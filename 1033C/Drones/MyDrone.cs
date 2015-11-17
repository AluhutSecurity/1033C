using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    public class MyDrone : INotifyPropertyChanged {
        public ulong UID { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime lastUpdate;
        public DateTime LastUpdate {
            get {
                return this.lastUpdate;
            }
            private set {
                this.lastUpdate = value;
                if ( this.PropertyChanged != null ) this.PropertyChanged( this, new PropertyChangedEventArgs( nameof( LastUpdate ) ) );
            }
        }

        private Delivery.MyPackage package;
        public Delivery.MyPackage Package {
            get {
                return this.package;
            }
            set {
                if ( this.package != value ) {
                    this.package = value;
                    this.LastUpdate = DateTime.Now;
                    if ( this.PropertyChanged != null ) this.PropertyChanged( this, new PropertyChangedEventArgs( nameof( Package ) ) );
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
                    if ( this.PropertyChanged != null ) this.PropertyChanged( this, new PropertyChangedEventArgs( nameof( State ) ) );
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
                    if ( this.PropertyChanged != null ) this.PropertyChanged( this, new PropertyChangedEventArgs( nameof( Position ) ) );
                }
            }
        }

        public float Capacity => Defines.MAX_PACKAGE_WEIGHT;

        public MyDrone( ulong uid ) {
            this.UID = uid;
            this.state = MyDroneState.Online;
            LastUpdate = DateTime.Now;
            this.Position = new Location.MyGPSPosition( 0, 0, 0 );
        }

        public MyDrone( ulong uid, MyDroneState initState ) : this( uid ) {
            this.state = initState;
        }
    }
}
