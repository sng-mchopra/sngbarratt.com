using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    /// <summary>
    ///     Pounds and ounces are used for the USPS provider.
    /// </summary>
    public struct PoundsAndOunces
    {
        public int Pounds { get; set; }
        public int Ounces { get; set; }
    }
}
