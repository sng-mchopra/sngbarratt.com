using jCtrl.Services.Core.Domain.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jCtrl.Services.Core.Domain
{
    public class Country
    {
        #region Properties
        [Key]
        [MinLength(2)]
        [MaxLength(2)]
        public string Code { get; set; } // ISO 3166 Alpha-2

        [MinLength(1)]
        [MaxLength(3)]
        public string InternationalDialingCode { get; set; }

        [Required]
        public bool IsEuropean { get; set; }

        [Required]
        public bool IsMemberOfEEC { get; set; }

        public ICollection<CountryTitle> Titles { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
