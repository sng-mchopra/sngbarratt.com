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
    public class CustomerEmailAddressRepository : Repository<CustomerEmailAddress>, ICustomerEmailAddressRepository
    {
        public CustomerEmailAddressRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<CustomerEmailAddress> GetCustomerEmailAddress(Guid custId, int emailId)
        {
            return await jCtrlContext.CustomerEmailAddresses
                .AsNoTracking()
                .Where(a => a.Customer_Id == custId)
                .Where(a => a.Id == emailId)
                .Cacheable()
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerEmailAddress>> GetCustomerEmailAddresses(Guid custId)
        {
            return await jCtrlContext.CustomerEmailAddresses
                .AsNoTracking()
                .Where(a => a.Customer_Id == custId)
                .Cacheable()
                .ToListAsync();
        }
    }
}
