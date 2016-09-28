using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IProductRepository : IRepository<BranchProduct>
    {
        IQueryable<BranchProduct> GetBranchProducts(int branchId, Guid id);
        Task<ICollection<ProductAlternative>> GetAlternativeProducts(int productId);
        Task<ICollection<ProductLink>> GetLinkedProducts(int productId);
        Task<ICollection<ProductSupersession>> GetSupersessionProducts(int productId);
        Task<ICollection<BranchProductOffer>> GetProductOffers(Guid id);
        IQueryable<BranchProduct> GetBranchProductsByBranchCodePartNumber(string siteCode, string partNumber);
        IQueryable<BranchProduct> GetProductsByBranchCodePhrase(string siteCode, int language, string phrase);
        IQueryable<BranchProduct> GetBranchClearanceProductsByBranchCode(string siteCode);
        IQueryable<BranchProduct> GetRandomBranchClearanceProductsByBranchCode(string siteCode);
        IQueryable<BranchProduct> GetBranchSpecialOfferProductsByBranchCode(string siteCode);
        IQueryable<BranchProduct> GetRandomSpecialOfferProductsByBranchCode(string siteCode);
        Task<BranchProduct> GetBranchProductByBranchIdProductId(int branchId, Guid productId);
        Task<BranchProduct> GetBranchProductByProductId(Guid productId);
    }
}
