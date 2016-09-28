using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class InterfacePhraseTranslation : AuditEntityBase
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        #endregion

        #region Navigation Properties
        [Required]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }

        [Required]
        [ForeignKey("InterfacePhrase")]
        public int InterfacePhrase_Id { get; set; }
        public virtual InterfacePhrase InterfacePhrase { get; set; }
        #endregion
    }
}
