using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICustomerEmailAddressRepository : IRepository<CustomerEmailAddress>
    {
        Task<IEnumerable<CustomerEmailAddress>> GetCustomerEmailAddresses(Guid custId);
        Task<CustomerEmailAddress> GetCustomerEmailAddress(Guid custId, int emailId);
    }
}
