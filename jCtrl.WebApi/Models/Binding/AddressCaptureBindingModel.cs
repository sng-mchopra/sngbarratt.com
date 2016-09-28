using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class AddressCaptureBindingModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50)]
        public string Country { get; set; }
        
    }
}