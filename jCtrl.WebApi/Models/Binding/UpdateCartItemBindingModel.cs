using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class UpdateCartItemBindingModel
    {
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }        

    }
}