using System.ComponentModel.DataAnnotations;

namespace jCtrl.Services.Core.Domain
{
    public class Currency
    {
        #region Properties
        [Key]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [MinLength(1)]
        [MaxLength(1)]
        public string Symbol { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
