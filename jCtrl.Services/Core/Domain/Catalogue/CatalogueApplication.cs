using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueApplication : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Model")]
        public int Model_Id { get; set; }
        public virtual CatalogueModel Model { get; set; }

        [Index]
        [Required]
        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        public virtual CatalogueCategory Category { get; set; }

        [Index] //TODO: should these be required ?
        [ForeignKey("Section")]
        public int Section_Id { get; set; }
        public virtual CatalogueCategory Section { get; set; }

        [Index]
        [ForeignKey("SubSection")]
        public int SubSection_Id { get; set; }
        public virtual CatalogueCategory SubSection { get; set; }

        [Index]
        [ForeignKey("Assembly")]
        public int Assembly_Id { get; set; }
        public virtual CatalogueAssembly Assembly { get; set; }
        #endregion
    }
}
