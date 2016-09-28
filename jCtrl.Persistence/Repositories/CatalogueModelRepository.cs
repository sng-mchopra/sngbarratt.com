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
    public class CatalogueModelRepository : Repository<CatalogueModel>, ICatalogueModelRepository
    {
        public CatalogueModelRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<CatalogueModel> GetCatalogueModelByFamilyIdModelId(int familyId, int modelId)
        {
            return await jCtrlContext.CatalogueModels
                .Where(m => m.IsActive)
                .Where(m => m.Family.IsActive)
                .Where(m => m.Family_Id == familyId)
                .Where(m => m.Id == modelId)
                .Include(m => m.Titles)
                .Include(m => m.Applications)
                .Include(m => m.Applications.Select(a => a.Category))
                .Include(m => m.Applications.Select(a => a.Category.Titles))
                .Include(m => m.Applications.Select(a => a.Section))
                .Include(m => m.Applications.Select(a => a.Section.Titles))
                .Include(m => m.Applications.Select(a => a.SubSection))
                .Include(m => m.Applications.Select(a => a.SubSection.Titles))
                .SingleOrDefaultAsync();
        }

        public IQueryable<CatalogueModel> GetCatalogueModelsByFamily(int familyId)
        {
            return jCtrlContext.CatalogueModels
                .Where(m => m.Family.IsActive)
                .Where(m => m.IsActive)
                .Where(m => m.Family_Id == familyId)
                .OrderBy(m => m.SortOrder)
                .AsQueryable();
        }
    }
}
