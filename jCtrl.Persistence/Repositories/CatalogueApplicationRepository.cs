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
    public class CatalogueApplicationRepository : Repository<CatalogueApplication>, ICatalogueApplicationRepository
    {
        public CatalogueApplicationRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public IQueryable<CatalogueApplication> GetCatalogueApplicationsByAssemblyIdFamilyId(int assemblyId, int familyId, int modelId)
        {
            return jCtrlContext.CatalogueApplications
                .Where(a => a.Assembly_Id == assemblyId)
                .Where(a => a.IsActive)
                .Where(a => a.Model.Id == modelId)
                .Where(a => a.Model.IsActive)
                .Where(a => a.Model.Family.Id == familyId)
                .Where(a => a.Model.Family.IsActive)
                .AsQueryable();
        }
    }
}
