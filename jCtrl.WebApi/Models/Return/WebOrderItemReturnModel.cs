using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class WebOrderItemReturnModel
    {        
        public Guid Id { get; set; }

        public short LineNo { get; set; }
        public string LineStatus { get; set; }

        public int? ProductId { get; set; }
        public string PartNumber { get; set; }
        public string PartTitle { get; set; }

        public string DiscountCode { get; set; }
        public int TaxRateCategory { get; set; }        

        public decimal PackedHeightCms { get; set; }
        public decimal PackedWidthCms { get; set; }
        public decimal PackedDepthCms { get; set; }

        public decimal PackedWeightKgs { get; set; }
        public decimal PackedWeightKgs_Volumetric { get; set; }


        public decimal RetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Surcharge { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal TaxPrice { get; set; }

        public int QuantityRequired { get; set; }        
        public int QuantityAllocated { get; set; }       
        public int QuantityBackOrdered { get; set; }
        public int QuantityPicked { get; set; }
        public int QuantityPacked { get; set; }
        public int QuantityInvoiced { get; set; }       
        public int QuantityCredited { get; set; }

    }
}