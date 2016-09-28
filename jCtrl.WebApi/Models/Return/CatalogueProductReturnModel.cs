using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CatalogueProductReturnModel
    {
    
        public int CatalogueProductId { get; set; }
        public string PartNumber { get; set; }  
        public string QuantityOfFit { get; set; }     
        public string FromBreakPoint { get; set; }
        public string ToBreakPoint { get; set; }

        public ProductListReturnModel Product { get; set; }
    }
}
