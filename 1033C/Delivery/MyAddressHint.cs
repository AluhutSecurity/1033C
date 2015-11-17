using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Delivery {

    /// <summary>
    /// hints to help the MyDeliveryDatabase to identify 
    /// an address by partial information
    /// </summary>
    public enum MyAddressHint {
        Name,
        Street,
        HouseNumber,
        PostalCode,
        Town
    }
}
