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

namespace jCtrl.Infrastructure.Repositories
{
    public class ShippingMethodRepository : Repository<ShippingMethod>, IShippingMethodRepository
    {
        public ShippingMethodRepository(jCtrlContext context) : base(context)
        {
        }


        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<IEnumerable<ShippingMethod>> GetShippingMethodByBranch(int branchId)
        {
            return await jCtrlContext.ShippingMethods
                .Where(s => s.Branch_Id == branchId)
                .Where(s => s.IsActive)
                .ToListAsync();
        }
    }
}
