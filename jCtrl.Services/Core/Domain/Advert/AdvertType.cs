using System.ComponentModel.DataAnnotations;

namespace jCtrl.Services.Core.Domain
{
    public class AdvertType
    {
        #region Properties
        [Key]
        [MinLength(1)]
        [MaxLength(1)]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        #endregion
    }
}
