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
using jCtrl.Services.Core.Domain.Catalogue;

namespace jCtrl.Infrastructure.Repositories
{
    public class CatalogueFamilyRepository : Repository<CatalogueFamily>, ICatalogueFamilyRepository
    {
        public CatalogueFamilyRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public IQueryable<CatalogueFamily> GetCatalogueFamilies()
        {
            return jCtrlContext.CatalogueFamilies
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .AsQueryable();
        }

        public async Task<CatalogueFamily> GetCatalogueFamily(int id)
        {
            return await jCtrlContext.CatalogueFamilies
                .Where(f => f.IsActive)
                .Where(f => f.Id == id)
                .Include(f => f.Titles)
                .Include(f => f.Models)
                .Include(f => f.Models.Select(m => m.Titles))
                .SingleOrDefaultAsync();
        }
    }
}
