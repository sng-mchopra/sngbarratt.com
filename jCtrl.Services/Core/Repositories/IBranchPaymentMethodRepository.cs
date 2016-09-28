using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IBranchPaymentMethodRepository : IRepository<BranchPaymentMethod>
    {
        Task<IEnumerable<BranchPaymentMethod>> GetPaymentMethodsByBranch(int branchId);
    }
}
