using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueCategoryIntroduction : TranslatedIntroduction
    {
        #region Properties
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        public virtual CatalogueCategory Category { get; set; }
        #endregion
    }
}
