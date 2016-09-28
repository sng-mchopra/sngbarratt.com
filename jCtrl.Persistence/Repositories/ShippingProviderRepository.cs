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
    public class ShippingProviderRepository : Repository<ShippingProvider>, IShippingProviderRepository
    {
        public ShippingProviderRepository(jCtrlContext context) : base(context)
        {
        }


        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<IEnumerable<ShippingProvider>> GetShippingProviders()
        {
            return await jCtrlContext.ShippingProviders
                .Where(p => p.IsActive)
                .ToListAsync();
        }
    }
}
