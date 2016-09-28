using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CatalogueCategoryTreeReturnModel
    {
        //public string Url { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int ProductCount { get; set; }

        public int ModelId { get; set; }

        //public CatalogueAssemblyReturnModel Assembly { get; set; }

        public string AssemblyUrl { get; set; }

        public ICollection<CatalogueCategoryTreeReturnModel> SubCategories { get; set; }
        


    }
}