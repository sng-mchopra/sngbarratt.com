using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueAssemblyTitle : TranslatedTitle
    {
        #region Properties
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
