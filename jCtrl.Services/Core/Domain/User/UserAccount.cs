using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Domain
{
    public class UserAccount : IdentityUser
    {
        public DateTime? LastLoginTimestampUtc { get; set; }

        [Required]
        public DateTime CreatedTimestampUtc { get; set; }

        [Required]
        public DateTime UpdatedTimestampUtc { get; set; }

        // linked customer account
        [Index]
        [ForeignKey("CustomerAccount")]
        public Guid? Customer_Id { get; set; }
        public virtual jCtrl.Services.Core.Domain.Customer.Customer CustomerAccount { get; set; }

        // navigation
        public virtual ICollection<jCtrl.Services.Core.Domain.Customer.Customer> ManagedCustomerAccounts { get; set; }

    }
}
