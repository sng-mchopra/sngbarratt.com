using jCtrl.Services.Core.Domain.Payment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class BranchPaymentMethod
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Index(name: "Idx_BranchPaymentMethod_SortOrder")]
        public int SortOrder { get; set; }

        [Required]
        [Index(name: "Idx_BranchPaymentMethod_Default")]
        public bool IsDefault { get; set; }

        [Required]
        [Index(name: "Idx_BranchPaymentMethod_Active")]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties 
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("PaymentMethod")]
        public string PaymentMethod_Code { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        #endregion
    }
}
