using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CustomerListReturnModel
    {
        public string Url { get; set; }        
               
        public string FullName { get; set; }
        public string CompanyName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public int DefaultBranchId { get; set; }


    }

}