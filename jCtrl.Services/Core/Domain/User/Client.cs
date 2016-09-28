using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string AllowedOrigin { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        public int RefreshTokenTTL { get; set; }

        [Required]
        public bool IsSecure { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<RefreshToken> Tokens { get; set; }

    }
}
