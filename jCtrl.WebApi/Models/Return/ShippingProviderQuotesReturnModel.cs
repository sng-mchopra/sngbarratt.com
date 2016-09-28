using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class ShippingProviderQuotesReturnModel : ShippingProviderReturnModel
    {
        
        public List<ShippingQuoteReturnModel> Quotes { get; set; }

    }
}