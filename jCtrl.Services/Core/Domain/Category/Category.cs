using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class Category : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int DistinctProductCount { get; set; }

        [MaxLength(50)]
        public string ImageFilename { get; set; }

        [Index]
        [Required]
        public int SortOrder { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        public ICollection<CategoryTitle> Titles { get; set; }

        public ICollection<CategoryIntroduction> Introductions { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<CategoryProduct> Products { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [ForeignKey("CategoryType")]
        public int CategoryType_Id { get; set; }
        public virtual CategoryType CategoryType { get; set; }

        [Index]
        [ForeignKey("Parent")]
        public int? Parent_Id { get; set; }
        public virtual Category Parent { get; set; }
        #endregion
    }
}
