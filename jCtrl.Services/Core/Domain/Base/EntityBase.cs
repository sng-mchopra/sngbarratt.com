using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace jCtrl.Services.Core.Domain
{
    public abstract class EntityBase
    {
        #region Properties
        [Required]
        [ConcurrencyCheck]
        public int RowVersion { get; set; }

        [Required]
        public DateTime CreatedTimestampUtc { get; set; }

        [Required]
        public DateTime UpdatedTimestampUtc { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }

    public abstract class AuditEntityBase : EntityBase
    {
        #region Properties
        [Required]
        public string CreatedByUsername { get; set; }

        [Required]
        public string UpdatedByUsername { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
