using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class VerifyPaymentCardBindingModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.CreditCard)]
        [MinLength(14)]
        [MaxLength(22)]
        public string CardNumber { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }
        [Required]
        public int ExpiryYear { get; set; }

        public int? StartMonth { get; set; }
        public int? StartYear { get; set; }

        public int? IssueNumber { get; set; }



    }
}