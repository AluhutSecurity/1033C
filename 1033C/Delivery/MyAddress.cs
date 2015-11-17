using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Delivery {

    /// <summary>
    /// represents address as deliverypoint
    /// packages with a MyAddress will be delivered 
    /// to MyAddress.LandingLocation
    /// 
    /// To receive a MyAddress instance, query the MyDeliveryDatabase
    /// </summary>
    public class MyAddress {
        public ulong UID { get; }

        public Location.MyGPSLocation LandingLocation { get; }

        public string Name { get; }

        public string Street { get; }

        public string HouseNumber { get; }

        public string PostalCode { get; }
        
        public string Town { get; }

        internal MyAddress( ulong uid, Location.MyGPSLocation landinglocation, string name, string street, string housenumber, string postalcode, string town ) {
            this.UID = uid;
            this.Name = name;
            this.LandingLocation = landinglocation;
            this.Street = street;
            this.PostalCode = postalcode;
            this.Town = town;
            this.HouseNumber = housenumber;
        }
    }
}
