using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class AddressLookupReturnModel : AddressReturnModel
    {

        public string QualityIndex { get; set; }
        public short MatchScore { get; set; }

    }
}