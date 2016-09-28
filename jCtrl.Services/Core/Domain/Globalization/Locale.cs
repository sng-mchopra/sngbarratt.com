using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class Locale
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string CurrencySymbol { get; set; }
        #endregion

        #region Navigation Properties
        [Required]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }
        #endregion
    }
}
