using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class RefreshToken
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Index]
        [Required]
        [ForeignKey("Client")]
        public Guid Client_Id { get; set; }

        [Index]
        [Required]
        [ForeignKey("UserAccount")]
        public string User_Id { get; set; }

        [Required]
        public DateTime IssuedTimestampUtc { get; set; }

        [Required]
        public DateTime ExpiresTimestampUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }


        public virtual Client Client { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
