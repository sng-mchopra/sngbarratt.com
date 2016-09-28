using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IPaymentCardsRepository : IRepository<PaymentCard>
    {
        Task<IEnumerable<PaymentCard>> GetPaymentCards(Guid customerId);
        Task<Customer> GetCustomerByPaymentCard(Guid id);
        Task<PaymentCard> GetPaymentCard(Guid custId, int cardId);

    }
}
