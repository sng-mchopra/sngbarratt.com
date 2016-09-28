using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class WebOrderListReturnModel
    {
        public string Url { get; set; }

        public Guid Id { get; set; }

        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }                

        public int BranchId { get; set; }        

        public Guid CustomerId { get; set; }
        public string CustomerTaxNo { get; set; }
        public string CustomerOrderRef { get; set; }

        public string DeliveryName { get; set; }
        public string DeliveryAddressLine1 { get; set; }
        public string DeliveryAddressLine2 { get; set; }
        public string DeliveryTownCity { get; set; }
        public string DeliveryCountyState { get; set; }
        public string DeliveryPostalCode { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryCountryCode { get; set; }
        public string DeliveryContactNumber { get; set; }

        public string ShippingMethod { get; set; }

        public decimal ShippingCharge { get; set; }
        public decimal ShippingTaxRate { get; set; }
        public decimal GoodsAtRate1 { get; set; }
        public decimal GoodsTaxRate1 { get; set; }
        public decimal GoodsAtRate2 { get; set; }
        public decimal GoodsTaxRate2 { get; set; }        
        public decimal GrandTotal { get; set; }               
        
        public string  Status { get; set; }


    }
}