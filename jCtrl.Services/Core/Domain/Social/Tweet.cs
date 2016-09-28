using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class Tweet
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Text { get; set; }

        [MaxLength(250)]
        public string LinkUrl { get; set; }

        [Required]
        public DateTime CreatedTimestampUtc { get; set; }
        [Required]
        public DateTime UpdatedTimestampUtc { get; set; }
        #endregion

        #region Navigation Properties 
        #endregion
    }
}
