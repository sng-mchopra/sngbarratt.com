using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface ICustomerVehiclesRepository : IRepository<CustomerVehicle>
    {
        Task<IEnumerable<CustomerVehicle>> GetCustomerVehicles(Guid custId);

    }
}
