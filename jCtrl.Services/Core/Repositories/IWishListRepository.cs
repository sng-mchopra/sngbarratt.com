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
    public interface IWishListRepository : IRepository<WishList>
    {
        Task<IEnumerable<WishList>> GetWishListsByCustomer(Guid custId);
        Task<WishList> GetWishListsByCustomer(Guid custId, Guid listId );
    }
}
