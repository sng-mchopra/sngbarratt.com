using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Vehicle
{
    public class Vehicle : EntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Marque")]
        public int Marque_Id { get; set; }
        public virtual VehicleMarque Marque { get; set; }

        [Index]
        [Required]
        [ForeignKey("Range")]
        public int Range_Id { get; set; }
        public virtual VehicleRange Range { get; set; }

        [Index]
        [Required]
        [ForeignKey("Model")]
        public int Model_Id { get; set; }
        public virtual VehicleModel Model { get; set; }

        [Index]
        [Required]
        [ForeignKey("ModelVariant")]
        public int ModelVariant_Id { get; set; }
        public virtual VehicleModelVariant ModelVariant { get; set; }

        [Index]
        [Required]
        [ForeignKey("Body")]
        public int Body_Id { get; set; }
        public virtual VehicleBodyVariant Body { get; set; }

        [Index]
        [Required]
        [ForeignKey("EngineType")]
        public int EngineType_Id { get; set; }
        public virtual VehicleEngineTypeVariant EngineType { get; set; }

        [Index]
        [Required]
        [ForeignKey("Engine")]
        public int Engine_Id { get; set; }
        public virtual VehicleEngineVariant Engine { get; set; }

        [Index]
        [Required]
        [ForeignKey("Transmission")]
        public int Transmission_Id { get; set; }
        public virtual VehicleTransmissionVariant Transmission { get; set; }

        [Index]
        [ForeignKey("Drivetrain")]
        public int? Drivetrain_Id { get; set; }
        public virtual VehicleDrivetrainVariant Drivetrain { get; set; }

        [Index]
        [ForeignKey("Steering")]
        public int? Steering_Id { get; set; }
        public virtual VehicleSteeringVariant Steering { get; set; }

        [Index]
        [ForeignKey("TrimLevel")]
        public int? TrimLevel_Id { get; set; }
        public virtual VehicleTrimLevelVariant TrimLevel { get; set; }
        #endregion
    }
}
