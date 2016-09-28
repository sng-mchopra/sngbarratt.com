using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Advert
{
    public class Advert : AuditEntityBase
    {
        public Advert()
        {

        }
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageFilename_Desktop { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageFilename_Device { get; set; }

        [MaxLength(150)]
        public string LinkUrl { get; set; }

        [MaxLength(25)]
        public string PlayerId { get; set; }
        [MaxLength(25)]
        public string VideoId { get; set; }

        [Index]
        [Required]
        public DateTime ExpiresUtc { get; set; }

        [Index]
        [Required]
        public bool IsPriority { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("AdvertType")]
        public string AdvertType_Id { get; set; }
        public virtual AdvertType AdvertType { get; set; }
        #endregion
    }
}
