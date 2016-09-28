using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetCustomerCart(Customer customer, bool withTracking = false);
        Task<CartItem> GetCustomerCartItem(Guid itemId, Guid custId);
        Task<CartItem> GetCartItem(Guid itemId);
    }
}
