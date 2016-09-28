using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IWishListItemRepository : IRepository<WishListItem>
    {
        Task<WishListItem> GetWishListItem(Guid itemId, Guid custId, Guid listId);
    }
}
