using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class BranchOpeningTime
    {
        #region Properties 
        [Key, Column(Order = 1)]
        public DayOfWeek Day { get; set; }

        [Required]
        public TimeSpan OpensUtc { get; set; }

        [Required]
        public TimeSpan ClosesUtc { get; set; }
        #endregion

        #region Navigation Properties 
        [Key, Column(Order = 0)]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }
        #endregion
    }
}
