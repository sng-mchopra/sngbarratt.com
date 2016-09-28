using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class ShowEvent : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string EventUrl { get; set; }

        [MaxLength(30)]
        public string Location { get; set; }

        [MaxLength(500)]
        public string MapUrl { get; set; }

        [MaxLength(50)]
        public string ImageFilename { get; set; }

        [Required]
        public ICollection<ShowEventDateTime> EventDateTimes { get; set; }

        [NotMapped]
        public DateTime StartDate
        {
            get { return EventDateTimes.OrderBy(dt => dt.StartsUtc).FirstOrDefault().StartsUtc; }
        }

        [NotMapped]
        public DateTime EndDate
        {
            get { return EventDateTimes.OrderByDescending(dt => dt.EndsUtc).FirstOrDefault().EndsUtc; }
        }

        [Required]
        public bool IsAttending { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }
        #endregion
    }
}
