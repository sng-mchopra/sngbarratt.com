using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueAssemblyNode : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string AnnotationRef { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public CatalogueAssemblyNode Parent { get; set; }

        public ICollection<CatalogueAssemblyNodeTitle> Titles { get; set; }
        public virtual ICollection<CatalogueAssemblyNode> Children { get; set; }
        public virtual ICollection<CatalogueAssemblyNodeProduct> Products { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Assembly")]
        public int Assembly_Id { get; set; }
        public virtual CatalogueAssembly Assembly { get; set; }
        #endregion
    }
}
