using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class InterfacePhrase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Context { get; set; }

        [Required]
        public ICollection<InterfacePhraseTranslation> Translations { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
