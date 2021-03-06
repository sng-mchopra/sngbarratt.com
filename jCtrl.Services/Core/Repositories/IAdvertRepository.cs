﻿using jCtrl.Services.Core.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Repositories
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<Advert> GetAdvert(Guid id);
    }
}
