using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public class ShipmentResponse: BaseResponse
    {

        public ShipmentResponse()
        {
            Shipments = new List<Shipment>();
            Errors = new List<UspsError>();
        }

        public List<Shipment> Shipments { get; set; }

        
        //public string ConsignmentNumber { get; set; }
        //public string ConsignmentLabel { get; set; }
        //public DateTime EstimatedDeliveryDate { get; set; }
        //public decimal PostagePrice { get; set; }
        //public Address Recipient { get; set; }
        //public string ReferenceNumber { get; set; }
    }
}
