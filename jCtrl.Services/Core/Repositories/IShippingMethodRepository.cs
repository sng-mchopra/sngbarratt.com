using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IShippingMethodRepository : IRepository<ShippingMethod>
    {
        Task<IEnumerable<ShippingMethod>> GetShippingMethodByBranch(int branchId);
    }
}
