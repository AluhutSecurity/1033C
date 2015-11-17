using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Delivery {

    /// <summary>
    /// Manages all packages within the app
    /// is responsible for package-coherency between database and local data
    /// </summary>
    public class MyPackageManager {
        private Dictionary<ulong, MyPackage> packages;
        private Database.MyDeliveryDatabase database;
        private Location.MyLocationServer locationServer;

        /// <summary>
        /// Manages all Packages
        /// responsible for coherency between app and database packages
        /// allows registration of new packages
        /// </summary>
        /// <param name="database">Deliverydatabase that contains package information</param>
        /// <param name="locationserver">Locationservice that allows flightpath calculation for package registration</param>
        /// <exception cref="ArgumentNullException">database and locationserver must not be null</exception>
        public MyPackageManager( Database.MyDeliveryDatabase database, Location.MyLocationServer locationserver ) {
            if ( database == null ) throw new ArgumentNullException( nameof( database ) );
            if ( locationserver == null ) throw new ArgumentNullException( nameof( locationserver ) );
            this.database = database;
        }

        /// <summary>
        /// registers new package
        /// package recipient and weight is needed in order to request a flightpath from the locationserver
        /// </summary>
        /// <param name="recipient">recipient of the package</param>
        /// <param name="weight">weight of the package</param>
        /// <returns>
        /// true: registration successful, valid flightpath for package was found
        /// false: registration failed; recipient is not reachable (e.g. package to heavy for given distance)
        /// </returns>
        /// <exception cref="ArgumentNullException">recipient must not be null</exception>
        /// <exception cref="ArgumentException">weight must be greater 0 and less than or equal to Defines.MAX_PACKAGE_WEIGHT</exception>
        public bool RegisterPackage( MyAddress recipient, float weight ) {
            if ( recipient == null ) throw new ArgumentNullException( nameof( recipient ) );
            if ( weight < 0 ) throw new ArgumentException( nameof( weight ) + "must be greater than zero." );
            if ( weight > Defines.MAX_PACKAGE_WEIGHT ) throw new ArgumentException( nameof( weight ) + "must be less or equal to maximum supported weight" );

            Location.MyFlightPath path = this.locationServer.CalculateFlightPath( recipient, weight );
            if ( path == null ) return false;

            var package = this.database.AddPackage( recipient, null, weight );
            package.FlightPath = path;
            this.packages.Add( package.UID, package );
            return true;
        }

        /// <summary>
        /// package lookup with given uid of package
        /// checks app data first, if no package is found
        /// the deliverydatabase will be queried for uid
        /// if the deliverydatabase contains a package, 
        /// the local app data will be refreshed for that package
        /// </summary>
        /// <param name="uid">uid of the package</param>
        /// <returns>
        /// valid package instance
        /// null if no known package matches uid
        /// </returns>
        public MyPackage GetPackageByUID( ulong uid ) {
            if ( !packages.ContainsKey( uid ) ) {
                MyPackage package = this.database.GetPackageByUID( uid );
                if ( package == null ) return null;
                this.packages.Add( package.UID, package );
                return package;
            }
            return packages[uid];
        }

        /// <summary>
        /// updates the state of a package with the given uid
        /// </summary>
        /// <param name="uid">uid of the package that will be updated</param>
        /// <param name="state">the new state of the package</param>
        /// <returns>
        /// true: state update successful
        /// false: no package with given uid was found
        /// </returns>
        public bool UpdatePackage( ulong uid, MyPackageState state ) {
            var package = this.GetPackageByUID( uid );
            if ( packages == null ) return false;
            package.State = state;
            database.UpdatePackage( uid, state );
            return true;
        }
    }
}
