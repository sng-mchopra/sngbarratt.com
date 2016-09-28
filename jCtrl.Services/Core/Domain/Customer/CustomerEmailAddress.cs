using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Customer
{
    public class CustomerEmailAddress : EmailAddress
    {
        #region Properties 
        [Required]
        public bool IsMarketing { get; set; }

        [Required]
        public bool IsBilling { get; set; }

        [Required]
        public bool IsVerified { get; set; }

        public Guid? VerificationToken { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }
        #endregion
    }
}
