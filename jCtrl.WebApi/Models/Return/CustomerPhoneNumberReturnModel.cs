using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CustomerPhoneNumberReturnModel: PhoneNumberReturnModel
    {

        public int Id { get; set; }

        public string Type { get; set; }
        
        public bool Default { get; set; }
    }
}