using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Binding
{
    public class CreateUserBindingModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [MaxLength(5)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(250)]        
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MinLength(6)]
        [MaxLength(50)]        
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [Required]        
        public int BranchId { get; set; }

        [Required]        
        public int LanguageId { get; set; }

    }
}