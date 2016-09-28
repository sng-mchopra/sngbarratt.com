using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Infrastructure.Repositories
{
    public class EventRepository : Repository<ShowEvent>, IEventRepository
    {
        public EventRepository(jCtrlContext context) : base(context)
        {
        }

        public IQueryable<ShowEvent> GetEventsByDateRange(DateTime start, DateTime end)
        {
            return jCtrlContext.Events
                .Where(e => e.IsActive)
                .Where(e => e.EventDateTimes.OrderBy(t => t.StartsUtc).FirstOrDefault().StartsUtc <= end)
                .Where(e => e.EventDateTimes.OrderByDescending(t => t.EndsUtc).FirstOrDefault().EndsUtc > start)
                .OrderBy(e => e.EventDateTimes.FirstOrDefault().StartsUtc)
                .AsQueryable();
        }

        public IQueryable<ShowEvent> GetUpcomingEvents()
        {
            return jCtrlContext.Events
                .Where(e => e.IsActive)
                .Where(e => e.EventDateTimes.OrderByDescending(t => t.EndsUtc).FirstOrDefault().EndsUtc > DateTime.Today)
                .OrderBy(e => e.EventDateTimes.FirstOrDefault().StartsUtc)
                .AsQueryable();
        }

        public async Task<ShowEvent> GetEvent(Guid id)
        {
            return await jCtrlContext.Events
                .Where(b => b.Id == id)
                .Include(e => e.Branch)
                .Include(e => e.EventDateTimes)
                .SingleOrDefaultAsync();
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }
    }
}
