using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CatalogueAssemblyReturnModel
    {
        public string Url { get; set; }
        public int AssemblyId { get; set; }
        public string Title { get; set; }

        //public int ProductCount { get; set; }

        public CatalogueIllustrationReturnModel Illustration { get; set; }
        
        public ICollection<CatalogueAssemblyNodeReturnModel> Nodes { get; set; }



    }
}