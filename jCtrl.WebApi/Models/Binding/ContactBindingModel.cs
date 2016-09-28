using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class ContactBindingModel: AddressBindingModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50)]
        public string Name { get; set; }
    
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        [MaxLength(250)]
        public string EmailAddress { get; set; }

    }
}