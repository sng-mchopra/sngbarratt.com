using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class ShowEventDateTime
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartsUtc { get; set; }

        [Required]
        public DateTime EndsUtc { get; set; }
        #endregion

        #region Navigation Properties
        [ForeignKey("Event")]
        public Guid Event_Id { get; set; }
        public virtual ShowEvent Event { get; set; }
        #endregion
    }
}
