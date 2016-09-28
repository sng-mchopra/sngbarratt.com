using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductDocument : EntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Filename { get; set; }

        [MaxLength(25)]
        public string Title { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }

        [Index]
        [Required]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Domain.Product.Product Product { get; set; }
        #endregion
    }
}
