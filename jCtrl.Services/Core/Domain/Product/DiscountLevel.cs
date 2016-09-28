using jCtrl.Services.Core.Utils.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace jCtrl.Services.Core.Domain
{
    public class DiscountLevel
    {
        #region Properties
        [Key]
        [MinLength(2)]
        [MaxLength(2)]
        public string Code { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Retail { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level1 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level2 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level3 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level4 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level5 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level6 { get; set; }
        #endregion 

        #region Navigation Properties
        #endregion
    }
}
