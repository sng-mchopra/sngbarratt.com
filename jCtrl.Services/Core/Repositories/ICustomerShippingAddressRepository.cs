using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICustomerShippingAddressRepository : IRepository<CustomerShippingAddress>
    {
        Task<IEnumerable<CustomerShippingAddress>> GetShippingAddressesByCustomer(Guid id);
        Task<CustomerShippingAddress> GetShippingAddressByCustomerIdAddressId(Guid custId, int addressId);
    }
}
