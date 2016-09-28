using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IOrderRepository : IRepository<WebOrder>
    {
        Task<WebOrder> GetOrder(Guid id);
        IQueryable<WebOrder> GetOrderByCustomerId(Guid custId);
        IQueryable<WebOrder> GetCurrentOrdersByCustomerId(Guid custId, bool isOngoing);
    }
}
