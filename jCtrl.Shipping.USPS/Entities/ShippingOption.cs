using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public class ShippingOption
    {

        public string ProviderRef { get; set; }
        public string Name { get; set; }
        public string DeliveryCommitment { get; set; }
        public int? Zones { get; set; }
        public decimal PublishedRate { get; set; }
        public decimal DiscountedRate { get; set; }
        public decimal TrackingCost { get; set; }
        
    }

}

