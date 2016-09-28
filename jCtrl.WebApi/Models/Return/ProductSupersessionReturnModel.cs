using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class ProductSupersessionReturnModel
    {
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public string PartNumber { get; set; }
        public string Title { get; set; }
    }
}