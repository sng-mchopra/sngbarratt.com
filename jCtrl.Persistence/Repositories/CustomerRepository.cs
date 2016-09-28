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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<Customer> GetCustomer(Guid id) 
        {
            return await jCtrlContext.Customers
                .Include(c => c.Branch)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerAsync(Guid custId)
        {
            return await jCtrlContext.Customers
                .AsNoTracking()
                .Cacheable()
                .SingleAsync(c => c.Id == custId);
        }

        public async Task<Customer> GetCustomerByShippingAddress(Guid id)
        {
            return await jCtrlContext.Customers
                .Include(c => c.Branch)
                .Include(c => c.ShippingAddresses)
                .SingleOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Customer> GetCustomer(Guid id, bool changeTracking, bool cache, params Expression<Func<Customer, object>>[] includeProperties)
        {
            var query = jCtrlContext.Customers.AsQueryable();

            if (changeTracking)
            {
                query.AsNoTracking();
            }

            foreach (var item in includeProperties)
            {
                query.Include(item);
            }

            if (cache)
            {
                query.Cacheable();
            }

            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<WebOrder> GetOrdersByCustomer(Guid custId, bool isOngoing)
        {
            return jCtrlContext.WebOrders
                .AsNoTracking()
                .Where(o => o.Customer_Id == custId && o.Status.IsOnGoing == isOngoing)
                .AsQueryable();
        }
    }
}
