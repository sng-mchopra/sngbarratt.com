using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class Contact : Address
    {
        #region Properties
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(250)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
