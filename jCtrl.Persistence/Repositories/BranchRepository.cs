using System;
using System.Linq.Expressions;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Repositories;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Order;
using System.Threading.Tasks;
using EFSecondLevelCache;

namespace jCtrl.Infrastructure.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(jCtrlContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Branch>> GetBranches()
        {
            return await jCtrlContext.Branches
                .Include(b => b.Currency)
                .OrderBy(b => b.SortOrder)
                .ToListAsync();
        }

        public IQueryable<Advert> GetBranchAdvertsByCode(string siteCode, bool isPriority)
        {
            return jCtrlContext.Adverts
                .Where(a => a.Branch.SiteCode == siteCode)
                .Where(a => a.IsActive)
                .Where(a => a.IsPriority)
                .Where(a => a.ExpiresUtc >= DateTime.UtcNow)
                .OrderBy(a => Guid.NewGuid())
                .AsQueryable();
        }

        public IQueryable<Customer> GetCustomersByBranchCode(string siteCode)
        {
            return jCtrlContext.Customers
                .Where(b => b.Branch.SiteCode == siteCode)
                .Include(b => b.Branch)
                .OrderBy(c => c.Id)
                .AsQueryable();
        }

        public IQueryable<ShowEvent> GetEventsByBranchCode(string siteCode)
        {
            return jCtrlContext.Events
                .Where(e => e.IsActive)
                .Where(e => e.Branch.SiteCode == siteCode)
                .OrderBy(e => e.EventDateTimes.FirstOrDefault())
                .AsQueryable();
        }

        public IQueryable<WebOrder> GetWebOrdersByBranchCode(string siteCode)
        {
            return jCtrlContext.WebOrders
                .Where(o => o.Branch.SiteCode == siteCode)
                .OrderBy(o => o.OrderDate)
                .ThenBy(o => o.OrderNo)
                .AsQueryable();
        }

        public IQueryable<BranchProduct> GetProductsByBranchCode(string siteCode)
        {
            return jCtrlContext.BranchProducts
                .Where(p => p.Branch.SiteCode == siteCode)
                .Where(p => p.IsActive)
                .Where(p => p.ProductDetails.IsActive)
                .Where(p => p.SpecialOffers.Where(o => o.IsActive == true).Any())
                .OrderBy(p => Guid.NewGuid())
                .AsQueryable();
        }

        public async Task<Branch> GetBranch(int id)
        {
            return await jCtrlContext.Branches
                .Where(b => b.Id == id)
                .Include(b => b.Currency)
                .Include(b => b.TaxRates)
                .Include(b => b.OpeningTimes)
                .Include(b => b.Introductions)
                .SingleOrDefaultAsync();
        }

        public async Task<Branch> GetBranchByCode(string siteCode)
        {
            return await jCtrlContext.Branches
                .Where(b => b.SiteCode == siteCode)
                .Include(b => b.Currency)
                .Include(b => b.TaxRates)
                .Include(b => b.OpeningTimes)
                .Include(b => b.Introductions)
                .SingleOrDefaultAsync();
        }

        public async Task<int> GetBranchLanguageIdAsync(int branchId)
        {
            return await jCtrlContext.Branches
                .AsNoTracking()
                .Cacheable()
                .Where(b => b.Id == branchId)
                .Select(b => b.Language_Id).SingleAsync();
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }
    }
}
