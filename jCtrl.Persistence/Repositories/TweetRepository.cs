using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Infrastructure.Repositories
{
    public class TweetRepository : Repository<Tweet>, ITweetRepository
    {
        public TweetRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public IQueryable<Tweet> GetTweets()
        {
            return jCtrlContext.Tweets
                .OrderByDescending(t => t.CreatedTimestampUtc)
                .AsQueryable();
        }

        public bool isTweetsTenMinutesOld()
        {
            return jCtrlContext.Tweets
                .FirstOrDefault()
                .UpdatedTimestampUtc < DateTime.UtcNow.AddMinutes(-10);
        }
    }
}
