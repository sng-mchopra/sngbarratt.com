using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using jCtrl.Services.Core.Domain;
using jCtrl.Infrastructure;

namespace jCtrl.WebApi.Infrastructure
{
    public class ApplicationUserManager : UserManager<UserAccount>
    {
        public ApplicationUserManager(IUserStore<UserAccount> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<jCtrlContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<UserAccount>(appDbContext));

            appUserManager.UserValidator = new UserValidator<UserAccount>(appUserManager)
            {
                //AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            appUserManager.EmailService = new jCtrl.WebApi.Services.EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<UserAccount>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }

        public override Task<IdentityResult> CreateAsync(UserAccount user, string password)
        {
            try
            {
                return base.CreateAsync(user, password);
            }
            catch (DbEntityValidationException ex)
            {

                // Retrieve the error messages as a list of strings.

                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message. 
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                throw ex;
            }
        }

    }
}