using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        Task<IEnumerable<UserAccount>> GetUsers(Guid custId);
        Task<UserAccount> GetUserByEmail(string email);
        Task<UserAccount> GetUserByIdCustomerId(Guid userId, Guid custId);
    }
}
