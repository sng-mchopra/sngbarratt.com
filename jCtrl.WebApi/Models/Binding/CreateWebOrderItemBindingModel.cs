using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class CreateWebOrderItemBindingModel
    {
        [Required]
        public Guid CartItemId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}