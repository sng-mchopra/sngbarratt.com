﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class VoucherType
    {
        #region Properties 
        [Key]
        [MinLength(1)]
        [MaxLength(1)]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
