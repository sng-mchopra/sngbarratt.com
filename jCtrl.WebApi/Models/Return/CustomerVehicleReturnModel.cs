using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CustomerVehicleReturnModel
    {

        public string DisplayName { get; set; }

        public VehicleReturnModel VehicleDetails {get; set;}

        public int ModelYear { get; set; }

        public string EngineNumber { get; set; }
        public string ChassisNumber { get; set; }
        public string VIN { get; set; }
        public string Notes { get; set; }        
        
        public bool Default { get; set; }

    }
}