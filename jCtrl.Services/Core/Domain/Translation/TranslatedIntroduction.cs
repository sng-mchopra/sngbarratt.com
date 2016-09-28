using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain
{
    public abstract class TranslatedIntroduction : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Intro { get; set; }

        public string More { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }
        #endregion
    }
}
