using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.WebApi.Models.Return
{
    public class ShippingQuoteReturnModel
    {
        public Guid Id { get; set; }
        public ShippingProviderReturnModel Provider { get; set; }
        //public string ProviderServiceRefernce { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
