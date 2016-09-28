using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class UpdateShippingAddressBindingModel: CreateShippingAddressBindingModel
    {
        [Required]
        public int Id { get; set; }
       
    }
}