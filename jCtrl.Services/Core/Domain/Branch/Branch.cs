using jCtrl.Services.Core.Utils.CustomAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class Branch : Contact
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3)]
        [MaxLength(3)]
        public string BranchCode { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(2)]
        [MaxLength(2)]
        public string SiteCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string FlagFilename { get; set; }

        [DecimalPrecision(9, 6)]
        public decimal Latitude { get; set; }

        [DecimalPrecision(9, 6)]
        public decimal Longitude { get; set; }

        [Index]
        [Required]
        public short SortOrder { get; set; }
        #endregion

        #region Navigation Properties 
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        [ForeignKey("Currency")]
        public string Currency_Code { get; set; }
        public virtual Currency Currency { get; set; }

        [Required]
        [ForeignKey("DefaultLanguage")]
        public int Language_Id { get; set; }
        public virtual Language DefaultLanguage { get; set; }

        public ICollection<BranchIntroduction> Introductions { get; set; }
        public virtual ICollection<Domain.Customer.Customer> Customers { get; set; }
        public virtual ICollection<BranchTaxRate> TaxRates { get; set; }
        public virtual ICollection<BranchOpeningTime> OpeningTimes { get; set; }
        public virtual ICollection<PackingContainer> PackingContainers { get; set; }
        public virtual ICollection<BranchPaymentMethod> PaymentMethods { get; set; }
        public virtual ICollection<ShippingMethod> ShippingMethods { get; set; }
        #endregion

        #region Methods 
        public BranchPaymentMethod DefaultPaymentMethod()
        {
            if (this.PaymentMethods != null)
            {
                return this.PaymentMethods
                    .Where(m => m.IsDefault == true && m.IsActive == true)
                    .FirstOrDefault();
            }

            return null;
        }
        #endregion
    }
}
