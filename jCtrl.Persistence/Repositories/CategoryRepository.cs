using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using jCtrl.Services.Core.Configuration;
using System.Threading.Tasks;
using EFSecondLevelCache;
using jCtrl.Services.Core.Domain.Branch;

namespace jCtrl.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(jCtrlContext context) : base(context)
        {
        }


        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<Category> GetCategoryAsync(Settings.CategoryTypes categoryType, int id)
        {
            return await jCtrlContext.Categories
                .AsNoTracking() 
                .Where(c => c.CategoryType_Id == (int)categoryType)
                .Where(c => c.Id == id)
                .Include(c => c.Titles)
                .Include(c => c.Introductions)
                .Cacheable()
                .SingleAsync();
        }

        public async Task<SortedPagedResultsReturnModel<BranchProduct>> GetProductsByCategoryAsync(int branchId, Settings.CategoryTypes categoryType, int? id, Settings.ProductListSortOptions orderBy, int pageSize, int pageNo, int lang)
        {
            try
            {
                var isActive = true;

                IQueryable<BranchProduct> query;

                if (id == null)
                {
                    query = jCtrlContext.Categories
                        .AsNoTracking() 
                        .Where(c => c.CategoryType_Id == (int)categoryType)
                        .Where(c => c.IsActive == isActive)
                        .Select(c => c.Products
                            .Where(cp => cp.IsActive == isActive)
                            .Select(cp => cp.ProductDetails.BranchProducts
                                .Where(bp => bp.Branch_Id == branchId && bp.IsActive == isActive && bp.BranchStatus_Code != "Z" && bp.BranchStatus_Code != "N")
                                .FirstOrDefault()
                            )
                            .FirstOrDefault()
                        )
                        .Distinct();
                }
                else
                {
                    query = jCtrlContext.Categories
                    .AsNoTracking() // turn off change tracking
                    .Where(c => c.CategoryType_Id == (int)categoryType && c.IsActive == isActive && (c.Id == id || c.Parent_Id == id))
                    .Select(c => c.Products
                       .Where(cp => cp.IsActive == isActive)
                       .Select(cp => cp.ProductDetails.BranchProducts
                            .Where(bp => bp.Branch_Id == branchId && bp.IsActive == isActive && bp.BranchStatus_Code != "Z" && bp.BranchStatus_Code != "N")
                            .FirstOrDefault()
                       )
                        .FirstOrDefault()
                    )
                    .Distinct();
                }

                // get total items
                var totalCount = query.Count();
                var results = new List<BranchProduct>();

                if (totalCount > 0)
                {

                    // set sort order
                    switch (orderBy)
                    {
                        case Settings.ProductListSortOptions.PartNumber:

                            query = query
                                 .OrderBy(p => p.ProductDetails.PartNumber);
                            break;

                        case Settings.ProductListSortOptions.ProductTitle:

                            // if translation doesn't exist this will use the English

                            query = query
                                .OrderBy(p => p.ProductDetails.TextInfo.Where(t => t.Language_Id == lang || t.Language_Id == 1).OrderByDescending(t => t.Language_Id).FirstOrDefault().ShortTitle)
                                .ThenBy(p => p.ProductDetails.PartNumber);

                            break;

                        case Settings.ProductListSortOptions.PriceHighLow:

                            // TODO: where a part has a supersession(s) use the some total of its item prices * s/s qty

                            query = query
                                .OrderByDescending(p =>
                                        p.SpecialOffers.Where(o => o.IsActive && o.ExpiryDate > DateTime.UtcNow).Any() ?
                                            (p.ClearancePrice > 0 && p.ClearancePrice < p.SpecialOffers.Where(o => o.IsActive).OrderBy(o => o.ExpiryDate).FirstOrDefault().OfferPrice) ?
                                                p.ClearancePrice : p.SpecialOffers.Where(o => o.IsActive).OrderBy(o => o.ExpiryDate).FirstOrDefault().OfferPrice
                                        :
                                            (p.ClearancePrice > 0 ? p.ClearancePrice : p.RetailPrice)

                                    )
                                .ThenBy(p => p.ProductDetails.PartNumber);

                            break;

                        case Settings.ProductListSortOptions.PriceLowHigh:

                            // TODO: Account for S/S - see above
                            query = query
                                .OrderBy(p =>
                                        p.SpecialOffers.Where(o => o.IsActive && o.ExpiryDate > DateTime.UtcNow).Any() ?
                                            (p.ClearancePrice > 0 && p.ClearancePrice < p.SpecialOffers.Where(o => o.IsActive).OrderBy(o => o.ExpiryDate).FirstOrDefault().OfferPrice) ?
                                                p.ClearancePrice : p.SpecialOffers.Where(o => o.IsActive).OrderBy(o => o.ExpiryDate).FirstOrDefault().OfferPrice
                                        :
                                            (p.ClearancePrice > 0 ? p.ClearancePrice : p.RetailPrice)

                                    )
                                .ThenBy(p => p.ProductDetails.PartNumber);

                            break;

                        case Settings.ProductListSortOptions.Popularity:

                            query = query
                                .OrderByDescending(p => p.WebOrderLines.Count())
                                .ThenBy(p => p.ProductDetails.PartNumber);

                            break;

                        default:

                            query = query
                               .OrderBy(p => p.ProductDetails.PartNumber);

                            break;

                    }

                    var pgSkip = (int)(pageSize * (pageNo - 1));
                    var pgTake = (int)(pageSize);

                    results = await query
                        .Skip(() => pgSkip) // use lamdas to force paramatisation
                        .Take(() => pgTake)
                        // it would be nice to be able to add Where to the includes to only bring back useful info
                        .Include(p => p.Branch.TaxRates)
                        .Include(p => p.ProductDetails.TextInfo)
                        .Include(p => p.ProductDetails.DiscountLevel)
                        .Include(p => p.ProductDetails.Images)
                        .Include(p => p.ProductDetails.SupersessionList
                            .Select(x => x.ReplacementProduct)
                            .Select(rp => rp.BranchProducts
                                .Select(bp => bp.ProductDetails.DiscountLevel)
                            )
                        )
                        .Include(p => p.SpecialOffers)
                        .Cacheable()
                        .ToListAsync();
                }

                int totalPages = (int)Math.Ceiling(((double)totalCount / (double)pageSize));

                var sortOptions = new List<KeyValuePair<string, int>>();
                foreach (var name in Enum.GetNames(typeof(Settings.ProductListSortOptions)))
                {
                    sortOptions.Add(new KeyValuePair<string, int>(name, (int)Enum.Parse(typeof(Settings.ProductListSortOptions), name)));
                }

                return new SortedPagedResultsReturnModel<BranchProduct>()
                {
                    PageSize = pageSize,
                    PageCount = totalPages,
                    PageNo = pageNo,
                    ResultsCount = totalCount,
                    Results = results,
                    SortOptions = sortOptions,
                    SortedBy = (int)orderBy
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Category>> GetRootCategoriesByTypeAsync(Settings.CategoryTypes categoryType)
        {
            return await jCtrlContext.Categories
                .AsNoTracking() 
                .Where(c => c.CategoryType_Id == (int)categoryType)
                .Where(c => c.Parent_Id == null)
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .Include(c => c.Titles)
                .Include(c => c.Children.Select(x => x.Titles))
                .Cacheable()
                .ToListAsync();
        }

        public async Task<List<Category>> GetSubCategoriesByParentAsync(Settings.CategoryTypes categoryType, int parent)
        {
            return await jCtrlContext.Categories
                .AsNoTracking() 
                .Where(c => c.CategoryType_Id == (int)categoryType)
                .Where(c => c.Parent_Id == parent)
                .Where(c => c.IsActive == true)
                .OrderBy(c => c.SortOrder)
                .Include(c => c.Titles)
                .Cacheable()
                .ToListAsync();
        }

        public async Task<int> RecountCategoryProductsAsync()
        {
            await jCtrlContext.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .AsQueryable()
                .Include(c => c.Products)
                   .Include(c => c.Products.Select(p => p.ProductDetails))
                   .Include(c => c.Children)
                   .Include(c => c.Children.Select(x => x.Products))
                   .Include(c => c.Children.Select(x => x.Products.Select(p => p.ProductDetails)))
                   .ForEachAsync(c => {
                       c.DistinctProductCount = GetProductIds(c, new List<int>()).Distinct().Count();
                       c.RowVersion++;
                       c.UpdatedByUsername = "RecountCategoryProductsAsync";
                       c.UpdatedTimestampUtc = DateTime.UtcNow;
                   });

            return await jCtrlContext.SaveChangesAsync();
        }

        private List<int> GetProductIds(Category category, List<int> lst)
        {
            if (category.Products != null)
            {
                foreach (CategoryProduct product in category.Products)
                {
                    if (product.IsActive)
                    {
                        // exclude turned down and unavailable
                        if (product.ProductDetails.PartStatus_Code != "Z" && product.ProductDetails.PartStatus_Code != "N") { lst.Add(product.Product_Id); }
                    }
                }
            }

            if (category.Children != null)
            {
                foreach (Category child in category.Children)
                {
                    lst = GetProductIds(child, lst);
                }
            }

            return lst;
        }
    }
}
