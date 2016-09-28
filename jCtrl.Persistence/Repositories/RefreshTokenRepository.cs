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
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var tokens = await jCtrlContext.RefreshTokens
                .Where(r => r.User_Id == token.User_Id && r.Client_Id == token.Client_Id)
                .ToListAsync();

            foreach (var tkn in tokens)
            {
                await RemoveRefreshToken(tkn);
            }

            jCtrlContext.RefreshTokens.Add(token);

            return await jCtrlContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await jCtrlContext.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await jCtrlContext.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                jCtrlContext.RefreshTokens.Remove(refreshToken);
                return await jCtrlContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            jCtrlContext.RefreshTokens.Remove(refreshToken);
            return await jCtrlContext.SaveChangesAsync() > 0;
        }
    }
}
