using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucher(string voucherCode);
    }
}
