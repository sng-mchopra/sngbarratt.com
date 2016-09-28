using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class ShippingProvider
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string LogoFilename { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
