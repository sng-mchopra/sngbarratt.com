using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueFamily : AuditEntityBase
    {
        #region Properties
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

        public ICollection<CatalogueFamilyTitle> Titles { get; set; }
        public virtual ICollection<CatalogueModel> Models { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
