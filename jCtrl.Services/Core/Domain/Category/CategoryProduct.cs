using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class CategoryProduct : AuditEntityBase
    {
        #region Properties
        [MaxLength(20)]
        public string QuantityOfFit { get; set; }
        [MaxLength(20)]
        public string FromBreakPoint { get; set; }
        [MaxLength(20)]
        public string ToBreakPoint { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties 
        [Key, Column(Order = 0)]
        [Index]
        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        public virtual Category Category { get; set; }

        [Key, Column(Order = 1)]
        [Index]
        [ForeignKey("ProductDetails")]
        public int Product_Id { get; set; }
        public virtual Domain.Product.Product ProductDetails { get; set; }
        #endregion
    }
}
