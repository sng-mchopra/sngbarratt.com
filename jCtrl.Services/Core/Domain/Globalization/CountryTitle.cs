using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Globalization
{
    public class CountryTitle : TranslatedTitle
    {
        #region Properties
        public string AlternativeSpellings { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("Country")]
        public string Country_Code { get; set; }
        public virtual Country Country { get; set; }
        #endregion
    }
}
