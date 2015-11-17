using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1033C.Delivery;

namespace _1033C.Database {
    /// <summary>
    /// Represents database connection to the delivery database
    /// access to package and address information/registration
    /// </summary>
    public class MyDeliveryDatabase {
        internal void UpdatePackage( ulong uid, MyPackageState state ) {
            throw new NotImplementedException();
        }

        internal MyPackage GetPackageByUID( ulong uid ) {
            throw new NotImplementedException();
        }

        internal MyPackage AddPackage( MyAddress recipient, MyAddress sender, float weight ) {
            throw new NotImplementedException();
        }

        public MyAddress GetAddress( ulong uid ) {
            throw new NotImplementedException();
        }

        public List<MyAddress> FindAddressesByHints( Dictionary<MyAddressHint, string> hints ) {
            throw new NotImplementedException();
        }

    }
}
