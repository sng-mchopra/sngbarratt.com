using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class UpdateEmailAddressBindingModel: CreateEmailAddressBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool Default { get; set; }

    }
}