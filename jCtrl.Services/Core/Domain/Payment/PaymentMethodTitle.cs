using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Payment
{
    public class PaymentMethodTitle : TranslatedTitle
    {
        #region Properties 
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("PaymentMethod")]
        public string PaymentMethod_Code { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        #endregion
    }
}
