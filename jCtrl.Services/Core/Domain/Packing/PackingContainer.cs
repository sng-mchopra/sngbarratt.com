using jCtrl.Services.Core.Utils.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain
{
    public class PackingContainer
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal InternalHeightCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal InternalWidthCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal InternalDepthCms { get; set; }

        [Required]
        public long InternalVolumeCm3 { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ExternalHeightCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ExternalWidthCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ExternalDepthCms { get; set; }

        [Required]
        public long ExternalVolumeCm3 { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal MaxWeightKgs { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal UnitWeightKgs { get; set; }

        public decimal WeightKgs_Volumetric()
        {
            return Utils.Calculator.CalculateVolumetricWeight_Kgs(ExternalWidthCms, ExternalHeightCms, ExternalDepthCms);
        }

        [Required]
        [DecimalPrecision(6, 3)]
        public decimal UnitPrice { get; set; }

        [Required]
        public bool IsUpsOnly { get; set; }

        [MaxLength(2)]
        public string UPS_Package_Ref { get; set; }

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
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }
        #endregion
    }
}
