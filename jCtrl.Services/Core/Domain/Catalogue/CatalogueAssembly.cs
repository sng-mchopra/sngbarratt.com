using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueAssembly : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public CatalogueAssemblyIllustration Illustration { get; set; }

        public ICollection<CatalogueAssemblyTitle> Titles { get; set; }
        public virtual ICollection<CatalogueApplication> Applications { get; set; }
        public virtual ICollection<CatalogueAssemblyNode> Nodes { get; set; }
        #endregion

        #region Navigation Properties 
        #endregion
    }
}
