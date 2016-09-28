using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class TaxRateReturnModel
    {
        public int TaxCategory { get; set; }

        public decimal Rate { get; set; }       

    }
}