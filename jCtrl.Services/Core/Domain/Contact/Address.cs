using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public abstract class Address : AuditEntityBase
    {
        #region Properties
        [Required]
        [MaxLength(35)]
        public string AddressLine1 { get; set; }
        [MaxLength(35)]
        public string AddressLine2 { get; set; }
        [MaxLength(30)]
        public string TownCity { get; set; }
        [MaxLength(30)]
        public string CountyState { get; set; }
        [MinLength(5)]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string CountryName { get; set; }
        [Required]
        public bool IsVerifiedAddress { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("Country")]
        public string Country_Code { get; set; }
        public virtual Country Country { get; set; }
        #endregion
    }
}
