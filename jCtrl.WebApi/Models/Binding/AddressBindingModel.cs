using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class AddressBindingModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(35)]
        public string AddressLine1 { get; set; }
                
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [MaxLength(35)]
        public string AddressLine2 { get; set; }

        [MaxLength(30)]
        public string TownCity { get; set; }

        [MaxLength(30)]
        public string CountyState { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MinLength(2)]
        [MaxLength(2)]
        public string CountryCode { get; set; }


    }
}