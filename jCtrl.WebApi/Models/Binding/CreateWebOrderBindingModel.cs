using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class CreateWebOrderBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string Site { get; set; }

        [Required]
        public List<CreateWebOrderItemBindingModel> Items { get; set; }

        public ShippingAddressReturnModel Recipient { get; set; }        

        [Required]
        public bool Collect { get; set; }

        [Required]
        public bool Quote { get; set; }

        public Guid ShippingId { get; set; } 

        [Required]
        public string PaymentCode { get; set; }

        public int PaymentCardId { get; set; }

        public string VoucherCode { get; set; }

        //[Required]
        //public decimal GoodsTotal { get; set; }

        //[Required]
        //public decimal ShippingTotal { get; set; }

        //[Required]
        //public decimal TaxTotal { get; set; }

        //[Required]
        //public decimal GrandTotal { get; set; }


    }
}
