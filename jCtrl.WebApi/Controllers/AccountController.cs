using jCtrl.Services;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.WebApi.Models.Binding;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("accounts")]
    public class AccountsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (ModelState.IsValid)
            {
                var branch = await unitOfWork.Branches.Get(createUserModel.BranchId);

                if (branch != null)
                {

                    var defPayMethod = await unitOfWork.BranchPaymentMethods
                        .SingleOrDefault(m => m.Branch_Id == branch.Id && m.IsDefault == true);

                    if (defPayMethod != null)
                    {
                        var user = new UserAccount()
                        {
                            UserName = createUserModel.Email,
                            Email = createUserModel.Email,
                            CustomerAccount = new Customer()
                            {
                                Title = createUserModel.Title,
                                FirstName = createUserModel.FirstName,
                                LastName = createUserModel.LastName,
                                Branch_Id = branch.Id,
                                Language_Id = createUserModel.LanguageId,
                                TradingTerms_Code = "RN",
                                PaymentMethod_Code = defPayMethod.PaymentMethod_Code,
                                EmailAddresses = new List<CustomerEmailAddress> {
                                        new CustomerEmailAddress {
                                            Address = createUserModel.Email,
                                            IsDefault = true,
                                            IsBilling = true,
                                            IsMarketing = true,
                                            RowVersion = 1,
                                            CreatedTimestampUtc = DateTime.UtcNow,
                                            CreatedByUsername = createUserModel.Email,
                                            UpdatedTimestampUtc = DateTime.UtcNow,
                                            UpdatedByUsername = createUserModel.Email
                                        }
                                    },
                                IsMarketingSubscriber = true,
                                IsPaperlessBilling = true,
                                IsActive = true,
                                RowVersion = 1,
                                CreatedTimestampUtc = DateTime.UtcNow,
                                CreatedByUsername = createUserModel.Email,
                                UpdatedTimestampUtc = DateTime.UtcNow,
                                UpdatedByUsername = createUserModel.Email
                            },
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow,
                        };

                        try
                        {
                            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

                            if (!addUserResult.Succeeded)
                            {
                                // filter out duplicate username error as we are forcing username to be the same as email
                                var errors = addUserResult.Errors.Where(e => e != "Name " + createUserModel.Email + " is already taken.");

                                // TODO: error translations, is there is a list of messages that appUserManager can throw ?
                                return GetErrorResult(new IdentityResult(errors));
                            }
                        }
                        catch (Exception ex)
                        {

                            System.Diagnostics.Debug.WriteLine(ex.Message);

                            return BadRequest("Unable to create user");
                        }


                        await SendAccountRegistrationEmail(user.Id, branch.SiteCode, createUserModel.LanguageId);


                        // TODO: Send user to webpage that will ask them to confirm their password to activate the account ??     


                        Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                        return Created(locationHeader, TheModelFactory.Create(user));
                    }

                    return BadRequest("Invalid branch payment method");
                }

                return BadRequest("Invalid branch");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private async Task<bool> SendAccountRegistrationEmail(string userId, string siteCode, int languageId)
        {
            var result = false;

            string confirmationToken = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(userId);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = userId, code = confirmationToken }));


            // use subject to specify the template and body to
            string subject = jCtrl.WebApi.Services.EmailService.EmailType.User_Create_Account.ToString();
            var values = new Dictionary<string, string>();

            values.Add("user", userId);
            values.Add("branch", siteCode);

            // Send language specific content
            // TODO: Could this content come out of the db and be editable in the admin area ???
            // or be templates setup on Mandrill
            switch (languageId)
            {
                case 1:// EN
                    values.Add("language", "EN");
                    values.Add("subject", "Account Registration EN");
                    values.Add("title", "Confirm your new User Account");
                    values.Add("body", "<p>Thanks for registering for an account at sngbarratt.com.<br /><br />Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">this link</a> to activate your account.<br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 2: // FR
                    values.Add("language", "FR");
                    values.Add("subject", "Account Registration FR");
                    values.Add("title", "Confirm your new User Account");
                    values.Add("body", "<p>Thanks for registering for an account at sngbarratt.com.<br /><br />Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">this link</a> to activate your account.<br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 3: // DE
                    values.Add("language", "DE");
                    values.Add("subject", "Account Registration DE");
                    values.Add("title", "Confirm your new User Account");
                    values.Add("body", "<p>Thanks for registering for an account at sngbarratt.com.<br /><br />Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">this link</a> to activate your account.<br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 4: // NL
                    values.Add("language", "NL");
                    values.Add("subject", "Account Registration NL");
                    values.Add("title", "Confirm your new User Account");
                    values.Add("body", "<p>Thanks for registering for an account at sngbarratt.com.<br /><br />Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">this link</a> to activate your account.<br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 5: // US
                    values.Add("language", "US");
                    values.Add("subject", "Account Registration US");
                    values.Add("title", "Confirm your new User Account");
                    values.Add("body", "<p>Thanks for registering for an account at sngbarratt.com.<br /><br />Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">this link</a> to activate your account.<br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                default:

                    values.Add("language", "EN");
                    values.Add("subject", "Account Registration DEF");
                    values.Add("title", "Confirm your new User Account");
                    values.Add("body", "<p>Thanks for registering for an account at sngbarratt.com.<br /><br />Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">this link</a> to activate your account.<br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
            }

            string body = JsonConvert.SerializeObject(values);

            try
            {

                await this.AppUserManager.SendEmailAsync(userId, subject, body);

                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                throw ex;
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("confirm", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {

                var user = await this.AppUserManager.FindByIdAsync(userId);


                var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);

                if (cust != null)
                {

                    var branch = await unitOfWork.Branches.Get(cust.Branch_Id);

                    if (branch != null)
                    {
                        return Redirect("http://www.sngbarratt.com/" + branch.SiteCode + "/account-confirmed.html");
                    }


                }

                // redirect user to confirmation page                
                return Redirect("http://www.sngbarratt.com/account-confirmed.html");
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("password")]
        public async Task<IHttpActionResult> ChangePassword(UpdatePasswordBindingModel model)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        IdentityResult result = await this.AppUserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);

                        if (!result.Succeeded)
                        {
                            return GetErrorResult(result);
                        }

                        // TODO: send email to user to notify that the password has been changed ???

                        return Ok();

                    }

                    return BadRequest("User not found");

                }
            }

            return Unauthorized();

        }

        [Authorize]
        [HttpPost]
        [Route("username")]
        public async Task<IHttpActionResult> ChangeUsername(UpdateUsernameBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = this.AppUserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                if (user.UserName == model.OldUsername)
                {
                    user.UserName = model.NewUsername;

                    IdentityResult result = await this.AppUserManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        return GetErrorResult(result);
                    }

                    return Ok();
                }

                return BadRequest("Username incorrect");
            }

            return NotFound();
        }


        [Authorize(Roles = "system_admin,accounts_author,accounts_reviewer")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize]
        [HttpGet]
        [Route("{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(Guid id)
        {

            var user = await this.AppUserManager.FindByIdAsync(id.ToString());

            if (user != null)
            {

                // restrict access to self and account admins
                if (user.UserName == User.Identity.Name || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                {
                    return Ok(this.TheModelFactory.Create(user));
                }

                return Unauthorized();
            }

            return NotFound();

        }

        [Authorize(Roles = "system_admin,accounts_author")]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            var user = await this.AppUserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }

        [Authorize(Roles = "system_admin,accounts_author")]
        [HttpPut]
        [Route("{id:guid}/roles")]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var user = await this.AppUserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await this.AppUserManager.GetRolesAsync(user.Id);

            var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(user.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(user.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        //[Authorize(Roles = "system_admin,accounts_author")]
        //[Route("users/{id:guid}/assignclaims")]
        //[HttpPut]
        //public async Task<IHttpActionResult> AssignClaimsToUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToAssign)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach (ClaimBindingModel claimModel in claimsToAssign)
        //    {
        //        if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
        //        {

        //            await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
        //        }

        //        await this.AppUserManager.AddClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
        //    }

        //    return Ok();
        //}

        //    //[Authorize(Roles = "system_admin,accounts_author")]
        //    //[Route("users/{id:guid}/removeclaims")]
        //    //[HttpPut]
        //    //public async Task<IHttpActionResult> RemoveClaimsFromUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToRemove)
        //    //{

        //    //    if (!ModelState.IsValid)
        //    //    {
        //    //        return BadRequest(ModelState);
        //    //    }

        //    //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    //    if (appUser == null)
        //    //    {
        //    //        return NotFound();
        //    //    }

        //    //    foreach (ClaimBindingModel claimModel in claimsToRemove)
        //    //    {
        //    //        if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
        //    //        {
        //    //            await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
        //    //        }
        //    //    }

        //    //    return Ok();
        //    //}



        //}
    }
}
