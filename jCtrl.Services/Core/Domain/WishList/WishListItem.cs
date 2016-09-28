using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.WishList
{
    public class WishListItem : AuditEntityBase
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string PartNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string PartTitle { get; set; }

        [Required]
        public int Quantity { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("WishList")]
        public Guid WishList_Id { get; set; }
        public virtual WishList WishList { get; set; }

        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }

        [Index]
        [Required]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Domain.Product.Product Product { get; set; }
        #endregion
    }
}
