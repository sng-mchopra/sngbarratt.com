using jCtrl.Services.Core.Utils.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain
{
    public class PackageManifestItem : EntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(20)]
        public string PartNumber { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal PackedHeightCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal PackedWidthCms { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal PackedDepthCms { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal PackedWeightKgs { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product.Product Product { get; set; }

        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("CountryOfOrigin")]
        public string CountryOfOrigin_Code { get; set; }
        public virtual Country CountryOfOrigin { get; set; }
        #endregion
    }
}
