﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Location {
    /// <summary>
    /// a gps location that can be targeted by the drone pilot 
    /// </summary>
    public class MyGPSPosition {
        public double Altitude { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }


        public static bool operator !=( MyGPSPosition a, MyGPSPosition b ) {
            if ( null == ( object ) a || null == ( object ) b ) return true;
            return ( a.Altitude != b.Altitude ) || ( a.Latitude != b.Latitude ) || ( a.Longitude != b.Longitude );
        }
        public static bool operator ==( MyGPSPosition a, MyGPSPosition b ) {
            if ( null == ( object ) a || null == ( object ) b ) return false;
            return ( a.Altitude == b.Altitude ) && ( a.Latitude == b.Latitude ) && ( a.Longitude == b.Longitude );
        }

        public override bool Equals( object obj ) => base.Equals( obj );

        public override int GetHashCode() => base.GetHashCode();

        public MyGPSPosition( double altitude, double latitude, double longitude ) {
            this.Altitude = altitude;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        private MyGPSPosition() { }

        internal static MyGPSPosition DeSerialize( byte[] data ) {
            var gps = new MyGPSPosition();
            gps.Altitude = BitConverter.ToDouble( data, 0 );
            gps.Latitude = BitConverter.ToDouble( data, sizeof( double ) );
            gps.Longitude = BitConverter.ToDouble( data, sizeof( double ) * 2 );
            return gps;
        }

        internal byte[] Serialize() => BitConverter.GetBytes( this.Altitude ).Concat( BitConverter.GetBytes( this.Latitude ) ).Concat( BitConverter.GetBytes( this.Longitude ) ).ToArray();
    }
}
