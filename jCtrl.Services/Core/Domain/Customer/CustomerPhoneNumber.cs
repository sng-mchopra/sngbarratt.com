using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Customer
{
    public class CustomerPhoneNumber : PhoneNumber
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [Required]
        public bool IsDefault { get; set; }
        #endregion

        #region Navigation Properties 
        [Required]
        [ForeignKey("PhoneNumberType")]
        public int PhoneNumberType_Id { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }

        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }
        #endregion
    }
}
