using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Order
{
    public class WebOrderEventNote : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }

        [Required]
        public Boolean Internal { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("WebOrderEvent")]
        public int WebOrderEvent_Id { get; set; }
        public virtual WebOrderEvent WebOrderEvent { get; set; }
        #endregion
    }
}
