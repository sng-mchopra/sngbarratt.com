using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Vehicle
{
    public class VehicleMarqueTitle : TranslatedTitle
    {
        #region Properties
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Marque")]
        public int Marque_Id { get; set; }

        public virtual VehicleMarque Marque { get; set; }
        #endregion
    }
}
