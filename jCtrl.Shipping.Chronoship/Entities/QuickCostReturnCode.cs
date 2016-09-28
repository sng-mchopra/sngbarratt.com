using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Shipping.Chronoship
{
    internal enum QuickCostReturnCode
    {
        Ok = 0,
        System_Error = 1,
        Data_Empty = 2,
        Invalid_Password = 3,
        Invalid_Service = 4,
        No_Price_Info = 5
    }
}
