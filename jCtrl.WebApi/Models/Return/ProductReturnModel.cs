using System;
using System.Collections.Generic;

namespace jCtrl.WebApi.Models.Return
{
    public class ProductReturnModel
    {
        public string Url { get; set; }

        public Guid Id { get; set; }        

        public string PartNumber { get; set; }
        
        public string Type {get;set;}

        public string Title { get; set; }
        public string Description { get; set; }        
        public string Information { get; set; }

        public string Applications { get; set; }
    
        public decimal RetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Surcharge { get; set; }
        public decimal TaxPrice { get; set; }        

        public string Status { get; set; }

        public ProductBrandReturnModel Brand { get; set; }
        public MeasurementsReturnModel Measurements { get; set; }
        public ProductOfferReturnModel SpecialOffer { get; set; }
        
        public ICollection<ProductImageReturnModel> Images { get; set; }
        public ICollection<ProductDocumentReturnModel> Documents { get; set; }
        public ICollection<ProductSupersessionReturnModel> Supersession { get; set; }
        public ICollection<ProductQuantityBreakPriceReturnModel> QuantityBreakPrices { get; set; }


        public ICollection<ProductListReturnModel> Alternatives { get; set; }
        public ICollection<ProductListReturnModel> Associations { get; set; }
        

    }
}