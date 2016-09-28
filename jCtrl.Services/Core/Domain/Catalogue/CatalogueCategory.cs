using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueCategory : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public ICollection<CatalogueCategoryTitle> Titles { get; set; }

        public ICollection<CatalogueCategoryIntroduction> Introductions { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
