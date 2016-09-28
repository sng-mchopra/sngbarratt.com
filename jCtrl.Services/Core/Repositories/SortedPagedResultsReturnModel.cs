using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public class SortedPagedResultsReturnModel<T>: PagedResultsReturnModel<T>
    {
        public ICollection<KeyValuePair<string, int>> SortOptions { get; set; }
        public int SortedBy { get; set; }
    }
}