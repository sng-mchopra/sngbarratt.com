using jCtrl.Services.Core.Utils.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class BranchTaxRate
    {
        #region Properties 
        [Required]
        [DecimalPrecision(5, 3)]
        public decimal Rate { get; set; }
        #endregion

        #region Navigation Properties
        [Key, Column(Order = 0)]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("TaxRateCategory")]
        public int TaxRateCategory_Id { get; set; }
        public virtual TaxRateCategory TaxRateCategory { get; set; }
        #endregion
    }
}
