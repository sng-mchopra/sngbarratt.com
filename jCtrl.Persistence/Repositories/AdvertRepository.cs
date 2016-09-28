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

namespace jCtrl.Infrastructure.Repositories
{
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        public AdvertRepository(jCtrlContext context) : base(context)
        {
        }
        public async Task<Advert> GetAdvert(Guid id)
        {
            return await jCtrlContext.Adverts
                .Where(a => a.Id == id)
                .Include(a => a.Branch)
                .SingleOrDefaultAsync();
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }
    }
}
