using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public abstract class PhoneNumber : AuditEntityBase
    {
        #region Properties
        [MinLength(3)]
        [MaxLength(3)]
        public string InternationalCode { get; set; }

        [Required]
        [MinLength(3)]
        public string AreaCode { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(6)]
        public string Number { get; set; }
        #endregion

        #region Navigation Properties
        #endregion

        #region Methods 
        public string FullNumber()
        {
            return (InternationalCode + " " + AreaCode + " " + Number).Replace("  ", " ").Trim();
        }
        #endregion
    }
}
