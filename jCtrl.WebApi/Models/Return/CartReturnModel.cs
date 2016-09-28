using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CartReturnModel
    {
        //public Guid CustomerId { get; set; }
        public string Site { get; set; }
        public bool ExpressCheckout { get; set; }
        public decimal CartTotal { get; set; }
        public int ItemCount { get; set; }        
        public List<CartItemReturnModel> Items {get; set;}

    }
}