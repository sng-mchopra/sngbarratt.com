using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class BranchAdvertReturnModel
    {

        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl_Desktop { get; set; }

        public string ImageUrl_Device { get; set; }

        public string LinkUrl { get; set; }

        public string PlayerId { get; set; }
        public string VideoId { get; set; }

    }
}