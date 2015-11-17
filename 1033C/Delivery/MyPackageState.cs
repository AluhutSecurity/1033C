using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Delivery {
    /// <summary>
    /// represents the state a given package is
    /// at any time the state of the package must be 
    /// ranged within this enumeration
    /// </summary>
    public enum MyPackageState {
        Lost = -1,          //verloren
        Registered = 0,     //Paket wurde an den Server gemeldet
        Ready = 1,          //Paket ist Versandbereit
        OnDelivery = 2,     //Paket wird ausgeliefert
        Delivered = 3       //Paket wurde erfolgreich abgeliefert
    }
}
