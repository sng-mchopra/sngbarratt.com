using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class EventListReturnModel
    {

        public string Url { get; set; }

        public string Title { get; set; }
        public string Location { get; set; }        

        public DateTime StartsUtc { get; set; }
        public DateTime EndsUtc { get; set; }

        public bool Attending { get; set; }

        public string BranchCode { get; set; }
    }
}