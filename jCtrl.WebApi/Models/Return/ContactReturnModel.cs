using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class ContactReturnModel: AddressReturnModel
    {

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

    }
}