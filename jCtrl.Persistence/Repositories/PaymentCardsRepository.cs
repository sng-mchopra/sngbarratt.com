using EFSecondLevelCache;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Payment;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jCtrl.Services.Core.Domain.Customer;

namespace jCtrl.Infrastructure.Repositories
{
    public class PaymentCardsRepository : Repository<PaymentCard>, IPaymentCardsRepository
    {
        public PaymentCardsRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<Customer> GetCustomerByPaymentCard(Guid id)
        {
            return await jCtrlContext.Customers
                .Where(c => c.Id == id)
                .Include(c => c.PaymentCards)
                .SingleOrDefaultAsync();
        }

        public async Task<PaymentCard> GetPaymentCard(Guid custId, int cardId)
        {
            return await jCtrlContext.PaymentCards
                .Where(c => c.Id == cardId)
                .Where(c => c.Customer_Id == custId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<PaymentCard>> GetPaymentCards(Guid customerId)
        {
            return await jCtrlContext.PaymentCards
                .AsNoTracking()
                .Where(c => c.Customer_Id == customerId)
                .Cacheable()
                .ToListAsync();
        }
    }
}
