using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CustomerEmailAddressReturnModel
    {        

        public int Id { get; set; }

        public string EmailAddress { get; set; }
        
        public bool Marketing { get; set; }
        
        public bool Billing { get; set; }

        public bool Verified { get; set; }

        public bool Default { get; set; }
        

    }
}