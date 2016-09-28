using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CatalogueModelReturnModel
    {
        public string Url { get; set; }
        public int ModelId { get; set; }
        public string Title { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public int ProductCount { get; set; }
        public ICollection<CatalogueCategoryTreeReturnModel> Categories { get; set; }
    }
}