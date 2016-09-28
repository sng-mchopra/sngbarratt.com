using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public int LanguageId { get; set; }
        public int BranchId { get; set; }
        

        public DateTime CreatedTimestamp { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }

}