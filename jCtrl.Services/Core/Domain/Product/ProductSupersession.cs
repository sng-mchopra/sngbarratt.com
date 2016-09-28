using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductSupersession : EntityBase
    {
        #region Properties 
        [Required]
        public int Quantity { get; set; }
        #endregion

        #region Navigation Properties 
        [Key, Column(Order = 0)]
        [Index]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }

        [Key, Column(Order = 1)]
        [Index]
        [ForeignKey("ReplacementProduct")]
        public int ReplacementProduct_Id { get; set; }
        public virtual Product ReplacementProduct { get; set; }
        #endregion
    }
}
