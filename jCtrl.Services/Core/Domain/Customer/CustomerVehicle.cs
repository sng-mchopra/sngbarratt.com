using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Customer
{
    public class CustomerVehicle : AuditEntityBase
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public short ModelYear { get; set; }

        [Required]
        [MaxLength(20)]
        public string DisplayName { get; set; }

        [MaxLength(20)]
        public string RegistrationNumber { get; set; }

        [MaxLength(20)]
        public string EngineNumber { get; set; }

        [MaxLength(20)]
        public string ChassisNumber { get; set; }

        [MaxLength(20)]
        public string VIN { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }

        [Index]
        [Required]
        public bool IsDefault { get; set; }

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
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }

        [Index]
        [Required]
        [ForeignKey("Vehicle")]
        public int Vehicle_Id { get; set; }
        public Domain.Vehicle.Vehicle Vehicle { get; set; }
        #endregion
    }
}
