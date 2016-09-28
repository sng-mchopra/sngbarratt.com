using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class ShippingAddressReturnModel: ContactReturnModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }
 
        public bool Default { get; set; }
    }
}