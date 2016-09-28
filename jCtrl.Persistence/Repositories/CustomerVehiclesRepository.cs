using EFSecondLevelCache;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using jCtrl.Services.Core.Domain.Order;

namespace jCtrl.Infrastructure.Repositories
{
    public class CustomerVehiclesRepository : Repository<CustomerVehicle>, ICustomerVehiclesRepository
    {
        public CustomerVehiclesRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<IEnumerable<CustomerVehicle>> GetCustomerVehicles(Guid custId)
        {
            return await jCtrlContext.CustomerVehicles
                .AsNoTracking()
                .Where(c => c.Customer_Id == custId)
                .Cacheable()
                .ToListAsync();
        }
    }
}
