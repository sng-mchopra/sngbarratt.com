using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CatalogueIllustrationReturnModel
    {
           
        public string ImageUrl_Thumbnail { get; set; }
        public string ImageUrl_Small { get; set; }
        public string ImageUrl_Medium { get; set; }
        public string ImageUrl_Large { get; set; }
        public string ImageUrl_Full { get; set; }
        
    }
}