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
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomer(Guid id);
        Task<Customer> GetCustomerAsync(Guid custId);
        Task<Customer> GetCustomerByShippingAddress(Guid id);
        Task<Customer> GetCustomer(Guid id, bool changeTracking, bool cache, params Expression<Func<Customer, object>>[] includeProperties);
        IQueryable<WebOrder> GetOrdersByCustomer(Guid custId, bool isOngoing);


    }
}
