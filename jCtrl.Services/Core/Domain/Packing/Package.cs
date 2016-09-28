using jCtrl.Services.Core.Utils.CustomAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain
{
    public abstract class Package : EntityBase
    {
        #region Properties
        [Key, Column(Order = 1)]
        public short PackageNo { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal HeightCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal WidthCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal DepthCms { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal WeightKgs { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal VolumetricWeightKgs { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Confidence_Volume { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Confidence_Weight { get; set; }

        [Required]
        public ICollection<PackageManifestItem> Manifest { get; set; }
        #endregion

        #region Navigation Properties
        [ForeignKey("PackingContainer")]
        public int? PackingContainer_Id { get; set; }
        public virtual PackingContainer PackingContainer { get; set; }
        #endregion

        #region Methods 
        public decimal Volume_Cm3()
        {
            return Services.Core.Utils.Calculator.CalculateVolume(WidthCms, HeightCms, DepthCms);
        }
        #endregion
    }
}
