using jCtrl.Services.Core.Domain.Order;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Voucher
{
    public class VoucherRedemption : EntityBase
    {
        #region Properties
        #endregion

        #region Navigation Properties
        [Key, Column(Order = 0)]
        [Index]
        [Required]
        [ForeignKey("Voucher")]
        public int Voucher_Id { get; set; }
        public virtual Domain.Voucher.Voucher Voucher { get; set; }

        [Key, Column(Order = 1)]
        [Index]
        [Required]
        [ForeignKey("WebOrder")]
        public Guid WebOrder_Id { get; set; }
        public virtual WebOrder WebOrder { get; set; }

        [Key, Column(Order = 2)]
        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }
        #endregion
    }
}
