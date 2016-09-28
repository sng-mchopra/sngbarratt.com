using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class OpeningTimeReturnModel
    {
        public DayOfWeek Day { get; set; }

        public TimeSpan OpensUtc { get; set; }

        public TimeSpan ClosesUtc { get; set; }

    }
}