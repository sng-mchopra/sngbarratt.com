using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class VoucherReturnModel: VoucherCheckoutReturnModel
    {

        public int Id { get; set; }   

        public int Branch_Id { get; set; }

        public Guid? Customer_Id { get; set; }
   
        public VoucherTypeReturnModel VoucherType { get; set; }

        public DateTime ValidFromUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public bool Active { get; set; }
    }
}
