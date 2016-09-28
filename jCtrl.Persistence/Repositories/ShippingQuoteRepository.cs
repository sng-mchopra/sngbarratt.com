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
using jCtrl.Services.Core.Domain.Shipping;

namespace jCtrl.Infrastructure.Repositories
{
    public class ShippingQuoteRepository : Repository<ShippingQuote>, IShippingQuoteRepository
    {
        public ShippingQuoteRepository(jCtrlContext context) : base(context)
        {
        }


        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<ShippingQuote> GetShippingQuote(Guid shippingId)
        {
            return await jCtrlContext.ShippingQuotes
                .Include(q => q.ShippingMethod)
                .Where(q => q.Id == shippingId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ShippingQuote>> GetShippingQuotesByCustomer(Guid custId)
        {
            return await jCtrlContext.ShippingQuotes
                .Where(q => q.Customer_Id == custId)
                .ToListAsync();
        }
    }
}
