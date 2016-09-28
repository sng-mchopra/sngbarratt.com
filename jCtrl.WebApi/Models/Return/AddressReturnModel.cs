using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class AddressReturnModel
    {

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string TownCity { get; set; }
        public string CountyState { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }

    }
}