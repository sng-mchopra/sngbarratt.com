using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Order
{
    public class WebOrderEvent : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<WebOrderEventNote> Notes { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("WebOrder")]
        public Guid WebOrder_Id { get; set; }
        public virtual WebOrder WebOrder { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("EventType")]
        public string WebOrderEventType_Id { get; set; }
        public virtual WebOrderEventType EventType { get; set; }
        #endregion
    }
}
