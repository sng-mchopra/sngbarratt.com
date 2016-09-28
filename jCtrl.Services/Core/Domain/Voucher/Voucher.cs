using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Voucher
{
    public class Voucher : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        [MinLength(14)]
        [MaxLength(14)]
        public string Code { get; set; } // xxxx-xxxx-xxxx

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal MinSpend { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal Value { get; set; }

        [Required]
        public DateTime ValidFromUtc { get; set; }

        [Index]
        [Required]
        public DateTime ValidToUtc { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<WebOrder> Redemptions { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }

        // optional
        [ForeignKey("Customer")]
        public Guid? Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }

        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("VoucherType")]
        public string VoucherType_Id { get; set; }
        public virtual VoucherType VoucherType { get; set; }
        #endregion
    }
}
