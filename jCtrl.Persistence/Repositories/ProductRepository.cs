using EFSecondLevelCache;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Product;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Infrastructure.Repositories
{
    public class ProductRepository : Repository<BranchProduct>, IProductRepository
    {
        public ProductRepository(jCtrlContext context) : base(context)
        {
        }

        public IQueryable<BranchProduct> GetBranchProducts(int branchId, Guid id)
        {
            return jCtrlContext.BranchProducts
                .AsNoTracking()
                .Where(b => b.Branch_Id == branchId)
                .Where(b => b.Id == id)
                .AsQueryable();
        }

        public async Task<ICollection<ProductAlternative>> GetAlternativeProducts(int productId)
        {
            return await jCtrlContext.ProductAlternatives
                .Where(p => p.Product_Id == productId)
                .Include(p => p.AlternativeProduct.TextInfo)
                .Include(p => p.AlternativeProduct.Images)
                .Include(p => p.AlternativeProduct.DiscountLevel)
                .Include(p => p.AlternativeProduct.BranchProducts)
                .ToListAsync();
        }

        public async Task<ICollection<ProductLink>> GetLinkedProducts(int productId)
        {
            return await jCtrlContext.ProductLinks
                .Where(p => p.Product_Id == productId)
                .Include(p => p.LinkedProduct.TextInfo)
                .Include(p => p.LinkedProduct.Images)
                .Include(p => p.LinkedProduct.DiscountLevel)
                .Include(p => p.LinkedProduct.BranchProducts)
                .ToListAsync();
        }

        public async Task<ICollection<ProductSupersession>> GetSupersessionProducts(int productId)
        {
            return await jCtrlContext.ProductSupersessions
                .Where(p => p.Product_Id == productId)
                .Include(p => p.ReplacementProduct.TextInfo)
                .Include(p => p.ReplacementProduct.Images)
                .Include(p => p.ReplacementProduct.DiscountLevel)
                .Include(p => p.ReplacementProduct.BranchProducts)
                .ToListAsync();
        }

        public async Task<ICollection<BranchProductOffer>> GetProductOffers(Guid id)
        {
            return await jCtrlContext.BranchProductOffers
                .Where(b => b.BranchProduct_Id == id)
                .Where(b => b.IsActive)
                .Where(b => b.ExpiryDate >= DateTime.Now)
                .Include(p => p.BranchProduct.ProductDetails.TextInfo)
                .Include(p => p.BranchProduct.ProductDetails.Images)
                .Include(p => p.BranchProduct.ProductDetails.DiscountLevel)
                .Include(p => p.BranchProduct.ProductDetails.BranchProducts)
                .ToListAsync();
        }

        public IQueryable<BranchProduct> GetBranchProductsByBranchCodePartNumber(string siteCode, string partNumber)
        {
            return jCtrlContext.BranchProducts
                .Where(p => p.Branch.SiteCode == siteCode && p.IsActive == true && p.ProductDetails.IsActive == true && (p.ProductDetails.PartNumber.StartsWith(partNumber) || p.ProductDetails.BasePartNumber.StartsWith(partNumber)))
                .OrderBy(p => p.ProductDetails.PartNumber)
                .AsQueryable();
        }

        public IQueryable<BranchProduct> GetProductsByBranchCodePhrase(string siteCode, int language, string phrase)
        {
            return jCtrlContext.BranchProducts
                .Where(p => p.Branch.SiteCode == siteCode && p.IsActive == true && p.ProductDetails.IsActive == true && (
                    p.ProductDetails.TextInfo.Where(i => i.Language_Id == language).FirstOrDefault().ShortTitle.Contains(phrase) ||
                    p.ProductDetails.TextInfo.Where(i => i.Language_Id == language).FirstOrDefault().LongTitle.Contains(phrase) ||
                    p.ProductDetails.TextInfo.Where(i => i.Language_Id == language).FirstOrDefault().ShortDescription.Contains(phrase) ||
                    p.ProductDetails.TextInfo.Where(i => i.Language_Id == language).FirstOrDefault().LongDescription.Contains(phrase) ||
                    p.ProductDetails.TextInfo.Where(i => i.Language_Id == language).FirstOrDefault().Keywords.Contains(phrase) ||
                    p.ProductDetails.TextInfo.Where(i => i.Language_Id == language).FirstOrDefault().SalesNotes.Contains(phrase)
                 ))
                 .OrderBy(p => p.ProductDetails.PartNumber)
                 .AsQueryable();
        }

        public IQueryable<BranchProduct> GetBranchClearanceProductsByBranchCode(string siteCode)
        {
            return jCtrlContext.BranchProducts
                .Where(p => p.Branch.SiteCode == siteCode && p.IsActive == true && p.ProductDetails.IsActive == true && p.ClearancePrice != 0 && p.Quantity != 0)
                .OrderBy(p => p.ProductDetails.PartNumber)
                .AsQueryable();
        }

        public IQueryable<BranchProduct> GetRandomBranchClearanceProductsByBranchCode(string siteCode)
        {
            return jCtrlContext.BranchProducts
                .Where(p => p.Branch.SiteCode == siteCode && p.IsActive == true && p.ProductDetails.IsActive == true && p.ClearancePrice != 0 && p.Quantity != 0)
                .OrderBy(p => Guid.NewGuid()) // use new guid to randomize the order
                .AsQueryable();
        }

        public IQueryable<BranchProduct> GetBranchSpecialOfferProductsByBranchCode(string siteCode)
        {
            return jCtrlContext.BranchProducts
                .Where(p => p.Branch.SiteCode == siteCode && p.IsActive == true && p.ProductDetails.IsActive == true && p.SpecialOffers.Where(o => o.IsActive == true).Any())
                .OrderBy(p => p.SpecialOffers.Where(o => o.IsActive == true).OrderBy(o => o.ExpiryDate).Select(o => o.ExpiryDate).FirstOrDefault())
                .AsQueryable();
        }

        public IQueryable<BranchProduct> GetRandomSpecialOfferProductsByBranchCode(string siteCode)
        {
            return jCtrlContext.BranchProducts
                            .Where(p => p.Branch.SiteCode == siteCode && p.IsActive == true && p.ProductDetails.IsActive == true && p.SpecialOffers.Where(o => o.IsActive == true).Any())
                            .OrderBy(p => Guid.NewGuid()) // use new guid to randomize the order
                            .AsQueryable();
        }

        public async Task<BranchProduct> GetBranchProductByBranchIdProductId(int branchId, Guid productId)
        {
            return await jCtrlContext.BranchProducts
                .Where(p => p.Branch_Id == branchId && p.Id == productId)
                .Include(p => p.ProductDetails)
                .SingleOrDefaultAsync();
        }

        public async Task<BranchProduct> GetBranchProductByProductId(Guid productId)
        {
            return await jCtrlContext.BranchProducts
                .AsNoTracking()
                .Where(p => p.Id == productId)
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductDetails.TextInfo)
                .Include(p => p.ProductDetails.DiscountLevel)
                .Include(p => p.SpecialOffers)
                .Cacheable()
                .SingleOrDefaultAsync();
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }
    }
}
