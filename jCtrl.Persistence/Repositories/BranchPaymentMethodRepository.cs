using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using jCtrl.Services.Core.Domain.Payment;
using jCtrl.Services.Core.Domain.Branch;

namespace jCtrl.Infrastructure.Repositories
{
    public class BranchPaymentMethodRepository : Repository<BranchPaymentMethod>, IBranchPaymentMethodRepository
    {
        public BranchPaymentMethodRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<IEnumerable<BranchPaymentMethod>> GetPaymentMethodsByBranch(int branchId)
        {
            return await jCtrlContext.BranchPaymentMethods
                .Include(m => m.PaymentMethod.Titles)
                .Where(m => m.Branch_Id == branchId && m.IsActive == true)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }
    }
}
