using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IEventRepository : IRepository<ShowEvent>
    {
        IQueryable<ShowEvent> GetEventsByDateRange(DateTime start, DateTime end);
        IQueryable<ShowEvent> GetUpcomingEvents();
        Task<ShowEvent> GetEvent(Guid id);
    }
}
