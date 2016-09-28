using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class PaymentOptionsReturnModel
    {

        public Guid CustomerId { get; set; }
        public string Default { get; set; }
        public List<PaymentMethodReturnModel> Methods { get; set; }     

    }
}