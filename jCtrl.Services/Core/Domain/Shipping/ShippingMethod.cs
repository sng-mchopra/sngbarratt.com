using jCtrl.Services.Core.Utils.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain
{
    public class ShippingMethod
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(10)]
        public string ProviderReference { get; set; }

        [Required]
        [MaxLength(35)]
        public string Title { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal MaxWeightKgs { get; set; }

        [Required]
        [DecimalPrecision(5, 1)]
        public decimal MaxDimensionCms { get; set; }

        [Required]
        public int MaxVolumeCm3 { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal CostPrice { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal Price { get; set; }

        public int? InternalMethodId { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch.Branch Branch { get; set; }

        [Required]
        [ForeignKey("ShippingProvider")]
        public int ShippingProvider_Id { get; set; }
        public virtual ShippingProvider ShippingProvider { get; set; }

        [Required]
        [ForeignKey("CoverageLevel")]
        [MaxLength(1)]
        public string ShippingCoverageLevel_Id { get; set; }
        public virtual ShippingCoverageLevel CoverageLevel { get; set; }
        #endregion
    }
}
