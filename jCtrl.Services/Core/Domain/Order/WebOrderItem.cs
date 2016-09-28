using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Order
{
    public class WebOrderItem : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public short LineNo { get; set; }

        [Index]
        [Required]
        [MaxLength(20)]
        public string PartNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string PartTitle { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string DiscountCode { get; set; }

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

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal RetailPrice { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal UnitPrice { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal Surcharge { get; set; }

        [Required]
        public int QuantityRequired { get; set; }
        [Required]
        public int QuantityAllocated { get; set; }
        [Required]
        public int QuantityBackOrdered { get; set; }
        [Required]
        public int QuantityPicked { get; set; }
        [Required]
        public int QuantityPacked { get; set; }
        [Required]
        public int QuantityInvoiced { get; set; }
        [Required]
        public int QuantityCredited { get; set; }

        public decimal LineTotal()
        {
            return ((UnitPrice + Surcharge) * QuantityRequired);
        }
        public decimal LineTotal_Allocated()
        {
            return ((UnitPrice + Surcharge) * QuantityAllocated);
        }
        public decimal LineTotal_BackOrdered()
        {
            return ((UnitPrice + Surcharge) * QuantityBackOrdered);
        }
        public decimal LineTotal_Picked()
        {
            return ((UnitPrice + Surcharge) * QuantityPicked);
        }
        public decimal LineTotal_Packed()
        {
            return ((UnitPrice + Surcharge) * QuantityPacked);
        }
        public decimal LineTotal_Invoiced()
        {
            return ((UnitPrice + Surcharge) * QuantityInvoiced);
        }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("WebOrder")]
        public Guid WebOrder_Id { get; set; }
        public virtual WebOrder WebOrder { get; set; }

        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("LineStatus")]
        public string WebOrderItemStatus_Id { get; set; }
        public virtual WebOrderItemStatus LineStatus { get; set; }

        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("CustomerLevel")]
        public string CustomerLevel_Id { get; set; }
        public CustomerTradingLevel CustomerLevel { get; set; }

        [Index]
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }

        [Index]
        [ForeignKey("BranchProduct")]
        public Guid? BranchProduct_Id { get; set; }
        public virtual Domain.Branch.BranchProduct BranchProduct { get; set; }

        [ForeignKey("ProductDetails")]
        public int? Product_Id { get; set; }
        public virtual Domain.Product.Product ProductDetails { get; set; }

        [Required]
        [ForeignKey("TaxRateCategory")]
        public int TaxRateCategory_Id { get; set; }
        public virtual TaxRateCategory TaxRateCategory { get; set; }
        #endregion
    }
}
