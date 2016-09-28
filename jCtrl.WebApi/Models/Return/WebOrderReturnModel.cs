using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class WebOrderReturnModel
    {
        public string Url { get; set; }

        public Guid Id { get; set; }

        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        
        public ICollection<WebOrderEventReturnModel> OrderEvents { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerTaxNo { get; set; }
        public string CustomerOrderRef { get; set; }


        public string InternalQuoteNo { get; set; }
        public DateTime? InternalQuoteDate { get; set; }

        public string InternalOrderNo { get; set; }
        public DateTime? InternalOrderDate { get; set; }


        public string BillingName { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingTownCity { get; set; }
        public string BillingCountyState { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCountry { get; set; }
        public string BillingCountryCode { get; set; }

        public string DeliveryName { get; set; }
        public string DeliveryAddressLine1 { get; set; }
        public string DeliveryAddressLine2 { get; set; }
        public string DeliveryTownCity { get; set; }
        public string DeliveryCountyState { get; set; }
        public string DeliveryPostalCode { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryCountryCode { get; set; }
        public string DeliveryContactNumber { get; set; }

        public ICollection<WebOrderItemReturnModel> Items { get; set; }

        public decimal EstimatedWeight { get; set; }

        public string ShippingMethod { get; set; }

        public string DiscountVocuher { get; set; }
        public decimal DiscountValue { get; set; }

        public decimal ShippingCharge { get; set; }
        public decimal ShippingTaxRate { get; set; }
        public decimal GoodsAtRate1 { get; set; }
        public decimal GoodsTaxRate1 { get; set; }
        public decimal GoodsAtRate2 { get; set; }
        public decimal GoodsTaxRate2 { get; set; }        
        public decimal TaxTotal { get; set; }
        public decimal GrandTotal { get; set; }
        
        public string PaymentMethod { get; set; }
        public string PaymentCard { get; set; }
        
        public string  Status { get; set; }


    }
}