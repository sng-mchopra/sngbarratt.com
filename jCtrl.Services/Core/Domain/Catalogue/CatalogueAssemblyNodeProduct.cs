using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueAssemblyNodeProduct : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        [MaxLength(20)]
        public string PartNumber { get; set; }

        [MaxLength(20)]
        public string QuantityOfFit { get; set; }
        [MaxLength(20)]
        public string FromBreakPoint { get; set; }
        [MaxLength(20)]
        public string ToBreakPoint { get; set; }

        public Domain.Product.Product ProductDetails { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [ForeignKey("Node")]
        public int Node_Id { get; set; }
        public virtual CatalogueAssemblyNode Node { get; set; }
        #endregion
    }
}
