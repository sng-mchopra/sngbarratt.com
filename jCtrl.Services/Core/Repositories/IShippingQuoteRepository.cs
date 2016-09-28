using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IShippingQuoteRepository : IRepository<ShippingQuote>
    {
        Task<ShippingQuote> GetShippingQuote(Guid shippingId);
        Task<IEnumerable<ShippingQuote>> GetShippingQuotesByCustomer(Guid custId);

    }
}
