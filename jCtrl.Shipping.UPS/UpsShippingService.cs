using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Shipping.UPS
{
    public enum UpsShippingService
    {
        UPS_Standard = 11,
        UPS_Ground = 3,
        UPS_3_Day_Select = 12,
        UPS_2nd_Day_Air = 2,
        UPS_2nd_Day_Air_AM = 59,
        UPS_Next_Day_Air_Saver = 13,
        UPS_Next_Day_Air = 1,
        UPS_Next_Day_Air_Early_AM = 14,
        UPS_Worldwide_Express = 7,
        UPS_Worldwide_Express_Plus = 54,
        UPS_Worldwide_Expedited = 8,
        UPS_Saver = 65
    }

}
