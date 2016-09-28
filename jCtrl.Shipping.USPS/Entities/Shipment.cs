using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public class Shipment
    {
        public int Id { get; set; }
        public string TrackingNo { get; set; }
        public decimal CostPrice { get; set; }
        public string Label { get; set; }
    }
}
