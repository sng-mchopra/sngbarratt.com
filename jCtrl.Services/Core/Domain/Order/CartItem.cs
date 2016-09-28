using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain
{
    public class CartItem : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Index]
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
        public DateTime ExpiresUtc { get; set; }

        [Required]
        public bool IsExpired { get; set; }

        [Required]
        public bool IsCheckedOut { get; set; }
        #endregion

        #region Navigation Properties 
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
        [Required]
        [ForeignKey("BranchProduct")]
        public Guid BranchProduct_Id { get; set; }
        public virtual BranchProduct BranchProduct { get; set; }
        #endregion

        #region Methods
        public decimal LineTotal()
        {
            return ((UnitPrice + Surcharge) * QuantityRequired);
        }

        public decimal LineTotal_ExcludingSurcharge()
        {
            return (UnitPrice * QuantityRequired);
        }
        #endregion
    }
}
