using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public class RatesResponse: BaseResponse
    {
        public RatesResponse()
        {
            ServiceOptions = new List<ShippingOption>();
            Errors = new List<UspsError>();
        }

        public List<ShippingOption> ServiceOptions { get; set; }        
    }
}
