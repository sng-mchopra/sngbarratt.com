using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class BranchIntroduction : TranslatedIntroduction
    {
        #region Properties 
        #endregion

        #region Navigation Properties
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }
        #endregion
    }
}
