using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.WebApi.Models.Binding
{
    public class CreateEmailAddressBindingModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string EmailAddress { get; set; }

        [Required]
        public bool Marketing { get; set; }

        [Required]
        public bool Billing { get; set; }

    }
}
