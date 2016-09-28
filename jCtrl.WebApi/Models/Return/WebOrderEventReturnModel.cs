using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class WebOrderEventReturnModel
    {        
        public string EventType { get; set; }

        public DateTime Timestamp { get; set; }
       
        public ICollection<WebOrderEventNoteReturnModel> Notes { get; set; }

    }
}