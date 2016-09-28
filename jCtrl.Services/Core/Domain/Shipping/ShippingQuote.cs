using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Shipping
{
    public class ShippingQuote : EntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(35)]
        public string RecipientName { get; set; }
        [Required]
        [MaxLength(35)]
        public string RecipientAddressLine1 { get; set; }
        [MaxLength(35)]
        public string RecipientAddressLine2 { get; set; }
        [MaxLength(30)]
        public string RecipientTownCity { get; set; }
        [MaxLength(30)]
        public string RecipientCountyState { get; set; }
        [MinLength(5)]
        [MaxLength(10)]
        public string RecipientPostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string RecipientCountryName { get; set; }
        [Required]
        [MaxLength(2)]
        public string RecipientCountryCode { get; set; }
        [MaxLength(20)]
        public string RecipientPhoneNumber { get; set; }

        [MaxLength(10)]
        public string ServiceReference { get; set; }

        [Required]
        [MaxLength(35)]
        public string ServiceDescription { get; set; }

        [Required]
        public short PackagesCount { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal EstimatedWeightKgs { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal CostPrice { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal Price { get; set; }

        // Not required to allow for instances where boxes can't be determined
        // [Required]
        public ICollection<ShippingQuotePackage> Packages { get; set; }
        #endregion

        #region Navigation Properties
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch.Branch Branch { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Customer.Customer Customer { get; set; }

        [Required]
        [ForeignKey("ServiceProvider")]
        public int ServiceProvider_Id { get; set; }
        public virtual ShippingProvider ServiceProvider { get; set; }

        [Required]
        [ForeignKey("ShippingMethod")]
        public int ShippingMethod_Id { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        #endregion
    }
}
