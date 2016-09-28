using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Catalogue
{
    public class CatalogueAssemblyIllustration
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Filename { get; set; }

        [MaxLength(50)]
        public string Filename_Thb { get; set; }
        [MaxLength(50)]
        public string Filename_Sml { get; set; }
        [MaxLength(50)]
        public string Filename_Med { get; set; }
        [MaxLength(50)]
        public string Filename_Lrg { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<CatalogueAssembly> Applications { get; set; }
        #endregion

        #region Navigation Properties
        #endregion

    }
}
