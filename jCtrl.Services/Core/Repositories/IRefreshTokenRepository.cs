using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
    }
}
