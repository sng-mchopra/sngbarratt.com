using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public abstract class PhoneNumberReturnModel
    {

        public string InternationalCode { get; set; }
        public string AreaCode { get; set; }
        public string Number { get; set; }

        public string FullNumber { get; set; }

    }
}