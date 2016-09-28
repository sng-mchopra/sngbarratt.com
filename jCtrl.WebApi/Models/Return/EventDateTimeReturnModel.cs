using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class EventDateTimeReturnModel
    {
        public DateTime StartsUtc { get; set; }
        public DateTime EndsUtc { get; set; }

    }
}