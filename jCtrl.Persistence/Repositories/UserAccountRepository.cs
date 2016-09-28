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
using EFSecondLevelCache;

namespace jCtrl.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<UserAccount> GetUserByEmail(string email)
        {
            return await jCtrlContext.Users
                .Where(u => u.UserName.ToLowerInvariant() == email)
                .SingleOrDefaultAsync();
        }

        public async Task<UserAccount> GetUserByIdCustomerId(Guid userId, Guid custId)
        {
            return await jCtrlContext.Users
                                .AsNoTracking()
                                .Where(u => u.Id == userId.ToString() && u.Customer_Id == custId)
                                .Cacheable()
                                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserAccount>> GetUsers(Guid custId)
        {
            return await jCtrlContext.Users
                .AsNoTracking()
                .Where(c => c.Customer_Id == custId)
                .Cacheable()
                .ToListAsync();
        }

    }
}
