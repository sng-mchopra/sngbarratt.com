using jCtrl.Services.Core.Domain.Catalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICatalogueAssemblyRepository : IRepository<CatalogueAssembly>
    {
        Task<CatalogueAssembly> GetCatalogueAssembly(int assemblyId);
    }
}
