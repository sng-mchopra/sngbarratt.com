using jCtrl.Services.Core.Domain.Catalogue;
using System.Linq;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICatalogueModelRepository : IRepository<CatalogueModel>
    {
        IQueryable<CatalogueModel> GetCatalogueModelsByFamily(int familyId);
        Task<CatalogueModel> GetCatalogueModelByFamilyIdModelId(int familyId, int modelId);
    }
}
