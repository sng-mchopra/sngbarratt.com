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
using jCtrl.Services.Core.Domain.Customer;
using EFSecondLevelCache;

namespace jCtrl.Infrastructure.Repositories
{
    public class CustomerShippingAddressRepository : Repository<CustomerShippingAddress>, ICustomerShippingAddressRepository
    {
        public CustomerShippingAddressRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<CustomerShippingAddress> GetShippingAddressByCustomerIdAddressId(Guid custId, int addressId)
        {
            return await jCtrlContext.CustomerShippingAddresses
                .AsNoTracking()
                .Where(a => a.Customer_Id == custId && a.Id == addressId)
                .Cacheable()
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerShippingAddress>> GetShippingAddressesByCustomer(Guid id)
        {
            return await jCtrlContext.CustomerShippingAddresses
                .AsNoTracking()
                .Where(a => a.Customer_Id == id)
                .Cacheable()
                .ToListAsync();
        }
    }
}
