using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<IEnumerable<Branch>> GetBranches();
        IQueryable<Advert> GetBranchAdvertsByCode(string siteCode, bool isPriority);
        IQueryable<Customer> GetCustomersByBranchCode(string siteCode);
        IQueryable<ShowEvent> GetEventsByBranchCode(string siteCode);
        IQueryable<WebOrder> GetWebOrdersByBranchCode(string siteCode);
        IQueryable<BranchProduct> GetProductsByBranchCode(string siteCode);


        Task<Branch> GetBranch(int id);
        Task<Branch> GetBranchByCode(string siteCode);
        Task<int> GetBranchLanguageIdAsync(int branchId);


    }
}
