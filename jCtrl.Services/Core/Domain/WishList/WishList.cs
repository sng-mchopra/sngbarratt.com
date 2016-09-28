using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.WishList
{
    public class WishList : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string DisplayName { get; set; }

        [Required]
        public ICollection<WishListItem> Items { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }
        #endregion
    }
}
