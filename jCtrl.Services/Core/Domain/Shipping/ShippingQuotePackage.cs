using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Shipping
{
    public class ShippingQuotePackage : Package
    {
        #region Properties
        #endregion

        #region Navigation Properties
        [Key, Column(Order = 0)]
        [Index]
        [ForeignKey("ShippingQuote")]
        public Guid ShippingQuote_Id { get; set; }
        public virtual ShippingQuote ShippingQuote { get; set; }
        #endregion
    }
}
