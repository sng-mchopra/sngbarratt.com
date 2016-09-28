using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class UpdateCartBindingModel
    {
        //public Guid CustomerId { get; set; }

        public string Site { get; set; }
        //public decimal CartTotal { get; set; }
        //public int ItemCount { get; set; }        
        public List<UpdateCartItemBindingModel> Items {get; set;}

    }
}