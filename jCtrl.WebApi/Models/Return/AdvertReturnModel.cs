using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class AdvertReturnModel
    {

        public Guid Id { get; set; }

        public int BranchId { get; set; }

        public string TypeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageFilename_Desktop { get; set; }

        public string ImageFilename_Device { get; set; }

        public string LinkUrl { get; set; }
        
        public string PlayerId { get; set; }
        
        public string VideoId { get; set; }
        
        public DateTime ExpiresUtc { get; set; }
        
        public bool Priority { get; set; }
        
        public bool Active { get; set; }

    }
}