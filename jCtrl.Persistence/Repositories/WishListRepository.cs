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
    public class WishListRepository : Repository<WishList>, IWishListRepository
    {
        public WishListRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<IEnumerable<WishList>> GetWishListsByCustomer(Guid custId)
        {
            return await jCtrlContext.WishLists
                    .AsNoTracking()
                    .Where(l => l.Customer_Id == custId)
                    .OrderBy(l => l.CreatedTimestampUtc)
                    .Include(l => l.Items.Select(i => i.Product.Images))
                    .Include(l => l.Items.Select(i => i.Product.TextInfo))
                    .Include(l => l.Items.Select(i => i.Product.BranchProducts))
                    .Cacheable()
                    .ToListAsync();
        }

        public async Task<WishList> GetWishListsByCustomer(Guid custId, Guid listId)
        {
            return await jCtrlContext.WishLists
                .AsNoTracking()
                .Where(l => l.Customer_Id == custId && l.Id == listId)
                .OrderBy(l => l.CreatedTimestampUtc)
                .Include(l => l.Items)
                .Include(l => l.Items.Select(i => i.Product.Images))
                .Include(l => l.Items.Select(i => i.Product.TextInfo))
                .Include(l => l.Items.Select(i => i.Product.BranchProducts))
                .Cacheable()
                .SingleOrDefaultAsync();
        }
    }
}
