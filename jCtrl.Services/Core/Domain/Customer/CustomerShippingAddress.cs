using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Customer
{
    public class CustomerShippingAddress : ShippingAddress
    {
        #region Properties 
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }
        #endregion
    }
}
