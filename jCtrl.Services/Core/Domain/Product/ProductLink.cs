using jCtrl.Services.Core.Domain.Branch;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductLink : EntityBase
    {
        #region Properties
        #endregion

        #region Navigation Properties 
        [Key, Column(Order = 0)]
        [Index]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Domain.Product.Product Product { get; set; }

        [Key, Column(Order = 1)]
        [Index]
        [ForeignKey("LinkedProduct")]
        public int LinkedProduct_Id { get; set; }
        public virtual Domain.Product.Product LinkedProduct { get; set; }
        #endregion

        #region Methods
        public BranchProduct LinkedBranchProduct(string branchCode)
        {
            return LinkedProduct.BranchProducts.FirstOrDefault(b => b.Branch.SiteCode == branchCode);
        }

        public BranchProduct LinkedBranchProduct(int branchId)
        {
            return LinkedProduct.BranchProducts.FirstOrDefault(b => b.Branch_Id == branchId);
        }
        #endregion
    }
}
