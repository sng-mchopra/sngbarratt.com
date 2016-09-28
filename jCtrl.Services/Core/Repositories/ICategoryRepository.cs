using jCtrl.Services.Core.Configuration;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Catalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetRootCategoriesByTypeAsync(Settings.CategoryTypes categoryType);
        Task<List<Category>> GetSubCategoriesByParentAsync(Settings.CategoryTypes categoryType, int parent);
        Task<Category> GetCategoryAsync(Settings.CategoryTypes categoryType, int id);
        Task<SortedPagedResultsReturnModel<BranchProduct>> GetProductsByCategoryAsync(int branchId, Settings.CategoryTypes categoryType, int? id, Settings.ProductListSortOptions orderBy, int pageSize, int pageNo, int lang);
        Task<int> RecountCategoryProductsAsync();
    }
}
