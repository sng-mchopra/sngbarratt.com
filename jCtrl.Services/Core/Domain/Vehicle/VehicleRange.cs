using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Vehicle
{
    public class VehicleRange : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        public string StartYear { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        public string EndYear { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public ICollection<VehicleRangeTitle> Titles { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
