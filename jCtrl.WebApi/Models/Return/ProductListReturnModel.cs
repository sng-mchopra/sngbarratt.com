using System;
using System.Collections.Generic;

namespace jCtrl.WebApi.Models.Return
{
    public class ProductListReturnModel
    {
        public string Url { get; set; }

        public Guid Id { get; set; }        

        public string PartNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal RetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Surcharge { get; set; }
        public decimal TaxPrice { get; set; }

        public string Status { get; set; }

        public ProductOfferReturnModel SpecialOffer { get; set; }
        public ProductImageReturnModel Image { get; set; }
        public ICollection<ProductSupersessionReturnModel> Supersession { get; set; }

           
    }
}