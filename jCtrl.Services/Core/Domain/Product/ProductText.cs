using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductText : AuditEntityBase
    {
        #region Properties 
        [Index]
        [Required]
        [MaxLength(20)]
        public string ShortTitle { get; set; }
        [MaxLength(100)]
        public string LongTitle { get; set; }
        [MaxLength(200)]
        public string ShortDescription { get; set; }
        [MaxLength(500)]
        public string LongDescription { get; set; }
        [MaxLength(200)]
        public string Keywords { get; set; }
        [MaxLength(200)]
        public string SalesNotes { get; set; }
        #endregion

        #region Navigation Properties
        [Key, Column(Order = 0)]
        [Index]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }

        [Key, Column(Order = 1)]
        [Index]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }
        #endregion
    }
}
