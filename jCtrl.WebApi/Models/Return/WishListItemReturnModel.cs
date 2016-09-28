using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class WishListItemReturnModel
    {

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string PartNumber { get; set; }
        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string Status { get; set; }

        public decimal RetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Surcharge { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPrice { get; set; }

        public int Quantity { get; set; }

        public decimal LineTotal { get; set; }

        public DateTime CreatedTimestamp { get; set; }
    }
}