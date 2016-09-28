using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class VehicleReturnModel
    {

        public int Id { get; set; }
        public string Marque { get; set; }
        public string Range { get; set; }
        public string Model { get; set; }
        public string ModelVariant { get; set; }
        public string Engine { get; set; }
        public string Transmission { get; set; }
        public string Drive { get; set; }
        public string BodyStyle { get; set; }
        public string TrimLevel { get; set; }        

    }
}