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
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(jCtrlContext context) : base(context)
        {
        }


        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<CartItem> GetCartItem(Guid itemId)
        {
            return await jCtrlContext.CartItems
                .Include(c => c.BranchProduct.ProductDetails)
                .Where(c => c.Id == itemId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCustomerCart(Customer customer, bool withTracking = false)
        {
            if (withTracking)
            {
                return await jCtrlContext.CartItems
                    .Where(i => i.Customer_Id == customer.Id
                        && i.Branch_Id == customer.Branch_Id
                        && i.IsCheckedOut == false
                        && i.IsExpired == false
                        && i.ExpiresUtc >= DateTime.UtcNow
                    )
                    .OrderBy(i => i.CreatedTimestampUtc)
                    .Include(i => i.Branch.TaxRates)
                    .Include(i => i.BranchProduct.ProductDetails.Images)
                    .ToListAsync();
            }

            return await jCtrlContext.CartItems
                .AsNoTracking()
                .Where(i => i.Customer_Id == customer.Id
                    && i.Branch_Id == customer.Branch_Id
                    && i.IsCheckedOut == false
                    && i.IsExpired == false
                    && i.ExpiresUtc >= DateTime.UtcNow
                )
                .OrderBy(i => i.CreatedTimestampUtc)
                .Include(i => i.Branch.TaxRates)
                .Include(i => i.BranchProduct.ProductDetails.Images)
                .Cacheable()
                .ToListAsync();
        }

        public async Task<CartItem> GetCustomerCartItem(Guid itemId, Guid custId)
        {
            return await jCtrlContext.CartItems
                .Where(i => i.Id == itemId && i.Customer_Id == custId)
                .SingleOrDefaultAsync();
        }
    }
}
