using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Vehicle
{
    public class VehicleSteeringVariantTitle : TranslatedTitle
    {
        #region Properties 
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Steering")]
        public int Steering_Id { get; set; }

        public virtual VehicleSteeringVariant Steering { get; set; }
        #endregion
    }
}
