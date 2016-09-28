using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Payment
{
    public class PaymentMethod
    {
        #region Properties 
        [Key]
        [MinLength(2)]
        [MaxLength(2)]
        public string Code { get; set; }
        
        [Required]
        [Index(name: "Idx_PaymentMethod_Active")]
        public bool IsActive { get; set; }

        public ICollection<PaymentMethodTitle> Titles { get; set; }
        #endregion

        #region Navigation Properties 
        #endregion
    }
}
