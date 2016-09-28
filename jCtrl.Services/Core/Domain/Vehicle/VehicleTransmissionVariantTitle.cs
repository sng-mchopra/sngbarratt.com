using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Vehicle
{
    public class VehicleTransmissionVariantTitle : TranslatedTitle
    {
        #region Properties
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Transmission")]
        public int Transmission_Id { get; set; }

        public virtual VehicleTransmissionVariant Transmission { get; set; }
        #endregion
    }
}
