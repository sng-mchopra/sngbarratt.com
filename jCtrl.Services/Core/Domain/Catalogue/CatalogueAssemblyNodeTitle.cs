using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueAssemblyNodeTitle : TranslatedTitle
    {
        #region Properties 
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Node")]
        public int Node_Id { get; set; }
        public virtual CatalogueAssemblyNode Node { get; set; }
        #endregion
    }
}
