using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class WishListReturnModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }

        public string Site { get; set; }
        
        public decimal ListTotal { get; set; }
        public int ItemCount { get; set; }        
        public List<WishListItemReturnModel> Items {get; set;}

    }
}