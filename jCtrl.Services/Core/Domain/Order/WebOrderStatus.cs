using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Order
{
    // S    Request submitted
    // R    Rejected by branch operative - quote or order
    // Q    Internal quote created
    // K    Pending customer approval
    // D    Quote declined by customer
    // E    Quote expired
    // A    Quote approved by customer
    // O    Internal order created
    // I    In progress (pick tickets or invoiced lines)
    // X    Order cancelled by customer
    // Z    Order failed payment
    // C    Order closed by branch operative (before being completed)
    // F    Order fulfilled / completed

    public class WebOrderStatus
    {
        #region Properties
        [Key]
        [MinLength(1)]
        [MaxLength(1)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsOnGoing { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
