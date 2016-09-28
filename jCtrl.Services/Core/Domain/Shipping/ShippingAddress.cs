using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public abstract class ShippingAddress : Contact
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string DisplayName { get; set; }

        [Index]
        [Required]
        public bool IsDefault { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
