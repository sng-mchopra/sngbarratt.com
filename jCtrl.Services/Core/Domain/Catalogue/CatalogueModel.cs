using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueModel : AuditEntityBase
    {
        #region Proprties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        public string StartYear { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        public string EndYear { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public ICollection<CatalogueModelTitle> Titles { get; set; }
        public virtual ICollection<CatalogueApplication> Applications { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Family")]
        public int Family_Id { get; set; }
        public virtual CatalogueFamily Family { get; set; }
        #endregion
    }
}
