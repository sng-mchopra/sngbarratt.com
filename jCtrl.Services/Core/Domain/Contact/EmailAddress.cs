using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public abstract class EmailAddress : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Address { get; set; }

        [Index]
        [Required]
        public bool IsDefault { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
