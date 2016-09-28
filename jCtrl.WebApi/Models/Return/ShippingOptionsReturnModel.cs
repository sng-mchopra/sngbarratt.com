using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.WebApi.Models.Return
{
    public class ShippingOptionsReturnModel
    {

        public int PackageCount { get; set; }

        public decimal EstimatedWeightKgs { get; set; }

        public ContactReturnModel Recipient { get; set; }

        public List<ShippingQuoteReturnModel> Quotes { get; set; }

    }
}
