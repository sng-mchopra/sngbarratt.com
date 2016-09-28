using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public abstract class BaseResponse
    {        
        public List<UspsError> Errors { get; set; }
    }
}
