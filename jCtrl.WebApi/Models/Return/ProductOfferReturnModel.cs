using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class ProductOfferReturnModel
    {

        public Guid ProductId { get; set; }

        public string Title { get; set; }

        public decimal OfferPrice { get; set; }        
        public DateTime OfferExpires { get; set; }

        public ProductImageReturnModel Image { get; set; }

    }
}