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
    public class PackingContainerRepository : Repository<PackingContainer>, IPackingContainerRepository
    {
        public PackingContainerRepository(jCtrlContext context) : base(context)
        {
        }


        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<IEnumerable<PackingContainer>> GetPackingContainersByBranch(int branchId)
        {
            return await jCtrlContext.PackingContainers
                .Where(b => b.Branch_Id == branchId)
                .Where(b => b.IsActive)
                .ToListAsync();
        }
    }
}
