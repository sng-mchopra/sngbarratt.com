using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Order
{
    // WS   Web Order Submitted
    // WA   Web Order Accepted
    // WD   Web Order Declined
    // WX   Web Order Deleted
    // QC   Sales Quote Created
    // QU   Sales Quote Updated
    // QA   Sales Quote Approved
    // QD   Sales Quote Declined
    // QE   Sales Quote Expired
    // QX   Sales Quote Deleted
    // OC   Sales Order Created
    // OU   Sales Order Updated
    // OD   Sales Order Cancelled
    // OX   Sales Order Deleted
    // PC   Pick Ticket Created
    // KC   Pack Ticket Created (Pick Ticket Confirmed)
    // IC   Sales Invoice Created
    // CC   Credit Note Created

    public class WebOrderEventType
    {
        #region Properties
        [Key]
        [MinLength(2)]
        [MaxLength(2)]
        public string Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
