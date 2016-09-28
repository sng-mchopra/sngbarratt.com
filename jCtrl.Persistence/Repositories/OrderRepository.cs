using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Infrastructure.Repositories
{
    public class OrderRepository : Repository<WebOrder>, IOrderRepository
    {
        public OrderRepository(jCtrlContext context) : base(context)
        {
        }

        public async Task<WebOrder> GetOrder(Guid id)
        {
            return await jCtrlContext.WebOrders
                .Where(o => o.Id == id)
                .Include(o => o.Branch)
                .Include(o => o.Items)
                .Include(o => o.OrderEvents.Select(e => e.EventType))
                .Include(o => o.OrderEvents.Select(e => e.Notes))
                .Include(o => o.PaymentMethod.Titles)
                .SingleOrDefaultAsync();
        }

        public IQueryable<WebOrder> GetOrderByCustomerId(Guid custId)
        {
            return jCtrlContext.WebOrders
                .AsNoTracking()
                .Where(o => o.Customer_Id == custId)
                .AsQueryable();
        }

        public IQueryable<WebOrder> GetCurrentOrdersByCustomerId(Guid custId, bool isOngoing)
        {
            return jCtrlContext.WebOrders
                .AsNoTracking() // turn off change tracking
                .Where(o => o.Customer_Id == custId && o.Status.IsOnGoing == isOngoing)
                .AsQueryable();
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }




    }
}
