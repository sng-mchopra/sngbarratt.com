using EFSecondLevelCache;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.WishList;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Infrastructure.Repositories
{
    public class WishListItemRepository : Repository<WishListItem>, IWishListItemRepository
    {
        public WishListItemRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<WishListItem> GetWishListItem(Guid itemId, Guid custId, Guid listId)
        {
            return await jCtrlContext.WishListItems
                .Where(i => i.Id == itemId && i.Customer_Id == custId && i.WishList_Id == listId)
                .SingleOrDefaultAsync();
        }
    }
}
