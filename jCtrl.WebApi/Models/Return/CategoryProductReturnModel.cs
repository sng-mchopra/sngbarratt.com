using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CategoryProductReturnModel
    {
        public string QuantityOfFit { get; set; }
        public string FromBreakPoint { get; set; }
        public string ToBreakPoint { get; set; }
        
        public ProductReturnModel Product { get; set; }
    }
}