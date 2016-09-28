using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueFamilyTitle : TranslatedTitle
    {
        #region Properties
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
