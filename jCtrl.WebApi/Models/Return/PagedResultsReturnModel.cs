using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.WebApi.Models.Return
{
    public class PagedResultsReturnModel<T>
    {

        public int[] PageSizes = { 5, 10, 25, 50, 100 }; //TODO: get these from a setting
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        

        public int ResultsCount { get; set; }
        public ICollection<T> Results { get; set; }


        public string PrevPageUrl { get; set; }
        public string NextPageUrl { get; set; }
        //public string LoadPageUrl { get; set; }
    }
}