using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using jCtrl.Services.Core.Utils.CustomAttributes;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Order;

namespace jCtrl.Services.Core.Domain.Product
{
    public class Product : EntityBase
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        [MaxLength(20)]
        public string PartNumber { get; set; }

        [Index]
        [Required]
        [MaxLength(20)]
        public string BasePartNumber { get; set; }

        [MinLength(6)]
        [MaxLength(14)]
        public string CommodityCode { get; set; }

        [MaxLength(4000)]
        public string ApplicationList { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ItemHeightCms { get; set; }
        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ItemWidthCms { get; set; }
        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ItemDepthCms { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal ItemWeightKgs { get; set; }

        public decimal ItemWeightKgs_Volumetric()
        {
            return jCtrl.Services.Core.Utils.Calculator.CalculateVolumetricWeight_Kgs(ItemWidthCms, ItemHeightCms, ItemDepthCms);
        }

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

        public decimal PackedWeightKgs_Volumetric()
        {
            return jCtrl.Services.Core.Utils.Calculator.CalculateVolumetricWeight_Kgs(PackedWidthCms, PackedHeightCms, PackedDepthCms);
        }

        [Index]
        [Required]
        public bool IsShipable { get; set; } // true - can be sent by post, false - collection only

        [Index]
        [Required]
        public bool IsShipableByAir { get; set; } // true - can be sent by air, false - can not be sent by air e.g. aerosols

        [Index]
        [Required]
        public bool IsPackable { get; set; } // true - can be packed in a box with other parts, false - shipped individually e.g. exhaust system

        [Index]
        [Required]
        public bool IsPackableLoose { get; set; } // true - can be packed loose, false - requires grouping together in a zip lock or jiffy bag e.g. nuts and bolts

        [Index]
        [Required]
        public bool IsRotatable { get; set; } // true - orientation can be rotated when packed, false - fixed orientation

        [Index]
        [Required]
        public bool IsQualityAssured { get; set; }

        [Index]
        [Required]
        public bool IsWebsiteApproved { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public ProductBrand ProductBrand { get; set; }

        [NotMapped]
        public ProductImage DefaultProductImage
        {
            get { return Images.FirstOrDefault(i => i.IsDefault == true); }
            set
            {
                foreach (ProductImage p in (Images.Where(i => i.IsDefault == true)))
                {
                    p.IsDefault = false;
                };

                value.IsDefault = true;
            }
        }
        public ICollection<ProductImage> Images { get; set; }

        public ICollection<ProductDocument> Documents { get; set; }

        [Required]
        public ICollection<ProductText> TextInfo { get; set; }
        #endregion

        #region Navigation Properties 
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("ProductType")]
        public string ProductType_Code { get; set; }
        public virtual ProductType ProductType { get; set; }

        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("CountryOfOrigin")]
        public string CountryOfOrigin_Code { get; set; }
        public virtual Country CountryOfOrigin { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("DiscountLevel")]
        public string DiscountLevel_Code { get; set; }
        public virtual DiscountLevel DiscountLevel { get; set; }

        [Required]
        [ForeignKey("TaxRateCategory")]
        public int TaxRateCategory_Id { get; set; }
        public virtual TaxRateCategory TaxRateCategory { get; set; }

        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("ComponentStatus")]
        public string ComponentStatus_Code { get; set; }
        public virtual ProductComponentStatus ComponentStatus { get; set; }

        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("PartStatus")]
        public string PartStatus_Code { get; set; }
        public virtual ProductStatus PartStatus { get; set; }

        [ForeignKey("Product_Id")]
        public virtual ICollection<BranchProduct> BranchProducts { get; set; }

        [ForeignKey("Product_Id")]
        public virtual ICollection<ProductSupersession> SupersessionList { get; set; }

        [ForeignKey("Product_Id")]
        public virtual ICollection<ProductAlternative> AlternativeProducts { get; set; }

        [ForeignKey("Product_Id")]
        public virtual ICollection<ProductLink> LinkedProducts { get; set; }

        [ForeignKey("Product_Id")]
        public virtual ICollection<ProductQuantityBreakDiscountLevel> QuantityBreakDiscountLevels { get; set; }

        [ForeignKey("Product_Id")]
        public virtual ICollection<WebOrderItem> WebOrderLines { get; set; }
        #endregion
    }
}
