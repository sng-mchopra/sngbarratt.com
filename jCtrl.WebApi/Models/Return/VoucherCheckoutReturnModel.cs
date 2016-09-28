using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class VoucherCheckoutReturnModel
    {
          
        public string Code { get; set; } // xxxx-xxxx-xxxx

        public string Title { get; set; }

        public decimal MinSpend { get; set; }

        public decimal Value { get; set; }

        public string Type { get; set; }

    }
}
