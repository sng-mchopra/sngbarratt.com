using jCtrl.Services.Core.Domain.Product;
using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class BranchProductOffer : EntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal OfferPrice { get; set; }

        [Index]
        [Required]
        public DateTime ExpiryDate { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("BranchProduct")]
        public Guid BranchProduct_Id { get; set; }
        public virtual BranchProduct BranchProduct { get; set; }

        [Required]
        [ForeignKey("Image")]
        public int ProductImage_Id { get; set; }
        public virtual ProductImage Image { get; set; }
        #endregion
    }
}
