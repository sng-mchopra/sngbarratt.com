using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class TweetReturnModel
    {
        public string Text { get; set; }
        public string LinkUrl { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}