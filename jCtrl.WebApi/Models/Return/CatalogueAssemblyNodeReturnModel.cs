using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CatalogueAssemblyNodeReturnModel
    {
        //public string Url { get; set; }
        public int AssemblyNodeId { get; set; }
        public string Title { get; set; }

        public string AnnotationRef { get; set; }

        //public int ProductCount { get; set; }

        public ICollection<CatalogueProductReturnModel> Products { get; set; }

        public ICollection<CatalogueAssemblyNodeReturnModel> Children { get; set; }



    }
}