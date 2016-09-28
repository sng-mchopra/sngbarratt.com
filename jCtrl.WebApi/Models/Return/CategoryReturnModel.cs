using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CategoryReturnModel
    {
        
        public int CategoryId { get; set; }

        public string Title { get; set; }               

        public IntroductionReturnModel Introduction { get; set; }

        public string ImageUrl { get; set; }

        public int ProductCount { get; set; }

        public string ProductsUrl { get; set; }

        public ICollection<CategoryReturnModel> SubCategories { get; set; }
        

    }
}