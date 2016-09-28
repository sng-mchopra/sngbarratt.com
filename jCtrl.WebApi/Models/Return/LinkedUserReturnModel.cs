using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class LinkedUserReturnModel
    {

        public string Id { get; set; }        
        public string Email { get; set; }
        public bool Confirmed { get; set; }
       
    }

}