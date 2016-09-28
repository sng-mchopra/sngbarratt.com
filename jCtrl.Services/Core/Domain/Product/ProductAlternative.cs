using jCtrl.Services.Core.Domain.Branch;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductAlternative : EntityBase
    {
        #region Properties
        #endregion

        #region Navigation Properties 
        [Key, Column(Order = 0)]
        [Index]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }

        [Key, Column(Order = 1)]
        [Index]
        [ForeignKey("AlternativeProduct")]
        public int AlternativeProduct_Id { get; set; }
        public virtual Product AlternativeProduct { get; set; }
        #endregion

        #region Methods
        public BranchProduct AlternativeBranchProduct(string branchCode)
        {
            return AlternativeProduct.BranchProducts.FirstOrDefault(b => b.Branch.SiteCode == branchCode);
        }
        public BranchProduct AlternativeBranchProduct(int branchId)
        {
            return AlternativeProduct.BranchProducts.FirstOrDefault(b => b.Branch_Id == branchId);
        }
        #endregion
    }
}
