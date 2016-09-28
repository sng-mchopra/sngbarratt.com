using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class EventReturnModel
    {
        public string Url { get; set; }
        
        public Guid Id { get; set; }
        
        public string Title { get; set; }        
        public string Description { get; set; }        
        public string EventUrl { get; set; }      
        public string Location { get; set; }        
        public string MapUrl { get; set; }        
        public string ImageUrl { get; set; }
        
        public ICollection<EventDateTimeReturnModel> DateTimes { get; set; }
                
        public bool Attending { get; set; }

        public string BranchCode { get; set; }
    }
}