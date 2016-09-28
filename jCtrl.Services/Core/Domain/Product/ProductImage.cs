using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductImage : EntityBase
    {
        public ProductImage() 
        {
            Id = 0;
            Filename = "AwaitingPhoto.jpg";
            IsDefault = true;
            IsActive = true;
        }

        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Filename { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsDefault { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
