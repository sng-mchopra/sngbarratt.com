using jCtrl.Services;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.WebApi.Models.Binding;
using jCtrl.WebApi.Models.Return;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using System.Data.Entity;
using EFSecondLevelCache;

namespace jCtrl.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("customers")]
    public class CustomersController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Customers
        [HttpGet]
        [Route("{custId:guid}", Name = "GetCustomerById")]
        [ResponseType(typeof(CustomerReturnModel))]
        public async Task<IHttpActionResult> GetCustomerByIdAsync([FromUri] Guid custId)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {
                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {

                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var customer = await unitOfWork.Customers.GetCustomer(custId, true, true,
                                c => c.Branch,
                                c => c.EmailAddresses,
                                c => c.Language,
                                c => c.PaymentCards,
                                c => c.PaymentMethod,
                                c => c.PhoneNumbers.Select(p => p.PhoneNumberType),
                                c => c.ShippingAddresses,
                                c => c.Vehicles);

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetCustomerByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            if (customer != null)
                            {
                                var cust = this.TheModelFactory.Create(customer);

                                var totalCount = 0;
                                var pageSize = 5;
                                var totalPages = 0;
                                IQueryable<WebOrder> query;

                                // get active orders
                                query = unitOfWork.Customers.GetOrdersByCustomer(custId, true);

                                // get total items
                                totalCount = query.Count();

                                if (totalCount > 0)
                                {
                                    var activeOrders = await query
                                    .OrderBy(o => o.OrderDate)
                                    .ThenBy(o => o.OrderNo)
                                    .Take(() => pageSize) // use lambda to force paramatisation
                                    .Cacheable()
                                    .ToListAsync();

                                    if (activeOrders != null)
                                    {
                                        if (activeOrders.Any())
                                        {

                                            // calc total pages
                                            totalPages = (int)Math.Ceiling(((double)totalCount / (double)pageSize));

                                            var urlHelper = new UrlHelper(Request);
                                            var nextLink = totalCount > pageSize ? urlHelper.Link("GetActiveWebOrdersByCustomerId", new { pageSize = pageSize, pageNo = 2 }) : null;

                                            cust.ActiveOrders = new PagedResultsReturnModel<WebOrderListReturnModel>()
                                            {
                                                PageSize = pageSize,
                                                PageCount = totalPages,
                                                PageNo = 1,
                                                Results = this.TheModelFactory.Create(activeOrders),
                                                ResultsCount = totalCount,
                                                PrevPageUrl = null,
                                                NextPageUrl = nextLink
                                            };
                                        }
                                    }
                                }

                                // get historic orders
                                query = unitOfWork.Customers.GetOrdersByCustomer(custId, false);

                                // get total items
                                totalCount = query.Count();

                                if (totalCount > 0)
                                {

                                    var historicOrders = await query
                                    .OrderByDescending(o => o.OrderDate)
                                    .ThenByDescending(o => o.OrderNo)
                                    .Take(() => pageSize) // use lambda to force paramatisation
                                    .Cacheable()
                                    .ToListAsync();

                                    if (historicOrders != null)
                                    {
                                        if (historicOrders.Any())
                                        {

                                            // calc total pages
                                            totalPages = (int)Math.Ceiling(((double)totalCount / (double)pageSize));

                                            var urlHelper = new UrlHelper(Request);
                                            var nextLink = totalCount > pageSize ? urlHelper.Link("GetHistoricWebOrdersByCustomerId", new { pageSize = pageSize, pageNo = 2 }) : null;

                                            cust.HistoricOrders = new PagedResultsReturnModel<WebOrderListReturnModel>()
                                            {
                                                PageSize = pageSize,
                                                PageCount = totalPages,
                                                PageNo = 1,
                                                Results = this.TheModelFactory.Create(historicOrders),
                                                ResultsCount = totalCount,
                                                NextPageUrl = nextLink
                                            };
                                        }
                                    }
                                }
                                return Ok(cust);
                            }
                            return NotFound();
                        }
                        return Unauthorized();
                    }
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/vehicles", Name = "GetVehiclesByCustomerId")]
        [ResponseType(typeof(List<CustomerVehicleReturnModel>))]
        public async Task<IHttpActionResult> GetVehiclesByCustomerIdAsync([FromUri] Guid custId, [FromUri] int language = -1)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var cars = await unitOfWork.CustomerVehicles.GetCustomerVehicles(custId);

                            if (cars.Any())
                            {

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetVehiclesByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(cars.ToList(), language));
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }
        #endregion

        #region "Email Addresses"

        [HttpGet]
        [Route("{custId:guid}/email", Name = "GetEmailAddressesByCustomerId")]
        [ResponseType(typeof(List<CustomerEmailAddressReturnModel>))]
        public async Task<IHttpActionResult> GetEmailAddressesByCustomerIdAsync([FromUri] Guid custId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var addresses = await unitOfWork.CustomerEmailAddresses.GetCustomerEmailAddresses(custId);

                            //if (addresses.Any())
                            //{

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetEmailAddressesByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            return Ok(this.TheModelFactory.Create(addresses.ToList()));
                            //}

                            //return NotFound();

                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        [HttpPost]
        [Route("{custId:guid}/email", Name = "CreateCustomerEmailAddress")]
        [ResponseType(typeof(CustomerEmailAddressReturnModel))]
        public async Task<IHttpActionResult> CreateCustomerEmailAddressAsync([FromUri] Guid custId, [FromBody] CreateEmailAddressBindingModel address)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            if (ModelState.IsValid)
                            {
                                var tmr = new System.Diagnostics.Stopwatch();
                                tmr.Start();

                                var cust = await unitOfWork.Customers.GetCustomer(custId, false, false,
                                    c => c.Branch,
                                    c => c.EmailAddresses);

                                if (cust != null)
                                {
                                    var email = new CustomerEmailAddress
                                    {
                                        Customer_Id = custId,
                                        Address = address.EmailAddress,
                                        IsBilling = address.Billing,
                                        IsMarketing = address.Marketing,
                                        IsVerified = false,
                                        VerificationToken = new Guid(),
                                        RowVersion = 1,
                                        CreatedTimestampUtc = DateTime.UtcNow,
                                        CreatedByUsername = user.UserName,
                                        UpdatedTimestampUtc = DateTime.UtcNow,
                                        UpdatedByUsername = user.UserName
                                    };

                                    try
                                    {
                                        cust.EmailAddresses.Add(email);

                                        // save email address
                                        await unitOfWork.CompleteAsync();

                                        // send email to verify email address ownership
                                        await SendEmailAddressAddedEmail(custId, email.Id, (Guid)email.VerificationToken, user.Id, cust.Branch.SiteCode, cust.Language_Id, email.Address, user.Email);

                                        return Ok(this.TheModelFactory.Create(email));
                                    }
                                    catch (Exception e)
                                    {
                                        System.Diagnostics.Debug.WriteLine(e.Message);

                                        throw e;
                                    }


                                }

                                return BadRequest("Customer not found.");

                            }
                            return BadRequest(ModelState);
                        }
                        return Unauthorized();
                    }
                }
            }


            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/email/{emailId:int}", Name = "GetEmailAddressByCustomerIdEmailId")]
        [ResponseType(typeof(CustomerEmailAddressReturnModel))]
        public async Task<IHttpActionResult> GetEmailAddressByCustomerIdEmailIdAsync([FromUri] Guid custId, [FromUri] int emailId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var email = await unitOfWork.CustomerEmailAddresses.GetCustomerEmailAddress(custId, emailId);

                            if (email != null)
                            {
                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetEmailAddressByCustomerIdEmailId took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(email));
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        [HttpPut]
        [Route("{custId:guid}/email/{emailId:int}", Name = "UpdateCustomerEmailAddress")]
        [ResponseType(typeof(CustomerEmailAddressReturnModel))]
        public async Task<IHttpActionResult> UpdateCustomerEmailAddressAsync([FromUri] Guid custId, [FromUri]int emailId, [FromBody] UpdateEmailAddressBindingModel address)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            if (ModelState.IsValid)
                            {
                                var tmr = new System.Diagnostics.Stopwatch();
                                tmr.Start();

                                var cust = await unitOfWork.Customers.GetCustomer(custId, false, false,
                                    c => c.Branch,
                                    c => c.EmailAddresses);

                                if (cust != null)
                                {
                                    var email = await unitOfWork.CustomerEmailAddresses.GetCustomerEmailAddress(custId, emailId);

                                    if (email != null)
                                    {

                                        //// 12/08/2016   Do not allow email address to be changed, force add new and delete old
                                        //if (email.Address.ToLowerInvariant() != address.EmailAddress.ToLowerInvariant())
                                        //{
                                        //    // email address has changed
                                        //    email.Address = address.EmailAddress;
                                        //    email.VerificationToken = new Guid();
                                        //    email.IsVerified = false;

                                        //    // send email to verify email address ownership
                                        //    await SendEmailAddressAddedEmail(custId, email.Id, (Guid)email.VerificationToken, user.Id, cust.Branch.SiteCode, cust.Language_Id, email.Address, user.Email);
                                        //}

                                        // update the other details
                                        email.IsBilling = address.Billing;
                                        email.IsMarketing = address.Marketing;
                                        email.RowVersion += 1;
                                        email.UpdatedTimestampUtc = DateTime.UtcNow;
                                        email.UpdatedByUsername = user.UserName;

                                        try
                                        {

                                            // save email address
                                            await unitOfWork.CompleteAsync();

                                            if (email.IsVerified && address.Default && cust.DefaultEmailAddress.Address.ToLowerInvariant() != email.Address.ToLowerInvariant())
                                            {
                                                // default address has changed

                                                email.IsDefault = true;

                                                cust.DefaultEmailAddress = email;

                                                // save customer account
                                                await unitOfWork.CompleteAsync();

                                            }

                                            tmr.Stop();
                                            System.Diagnostics.Debug.WriteLine("UpdateEmailAddressByCustomerIdAddressId took " + tmr.ElapsedMilliseconds + " ms");

                                            return Ok(this.TheModelFactory.Create(email));

                                        }
                                        catch (Exception e)
                                        {
                                            System.Diagnostics.Debug.WriteLine(e.Message);

                                            throw e;
                                        }



                                    }

                                    return NotFound();

                                }

                                return BadRequest("Customer not found.");

                            }
                            return BadRequest(ModelState);
                        }
                        return Unauthorized();
                    }
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{custId:guid}/email/{emailId:int}", Name = "DeleteEmailAddressByCustomerIdAddressId")]
        [ResponseType(typeof(List<CustomerEmailAddressReturnModel>))]
        public async Task<IHttpActionResult> DeleteEmailAddressByCustomerIdAddressIdAsync([FromUri] Guid custId, [FromUri] int emailId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var email = await unitOfWork.CustomerEmailAddresses.GetCustomerEmailAddress(custId, emailId);

                            if (email != null)
                            {

                                unitOfWork.CustomerEmailAddresses.Remove(email);
                                await unitOfWork.CompleteAsync();

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("DeleteEmailAddressByCustomerIdAddressId took " + tmr.ElapsedMilliseconds + " ms");

                                return await GetEmailAddressesByCustomerIdAsync(custId);
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        // TODO: do we want this to be anonymous ???
        [AllowAnonymous]
        [HttpGet]
        [Route("{custId:guid}/email/{emailId:int}/verify/{token:guid}", Name = "VerifyEmailAddressByCustomerIdEmailIdToken")]
        [ResponseType(typeof(CustomerEmailAddressReturnModel))]
        public async Task<IHttpActionResult> VerifyEmailAddressByCustomerIdEmailIdTokenAsync([FromUri] Guid custId, [FromUri] int emailId, [FromUri] Guid token)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var email = await unitOfWork.CustomerEmailAddresses.GetCustomerEmailAddress(custId, emailId);

            var cust = await unitOfWork.Customers.GetCustomer(custId, false, false);

            if (email != null && cust != null)
            {

                if (email.VerificationToken == token)
                {
                    // token matches

                    email.VerificationToken = null;
                    email.IsVerified = true;

                    if (email.IsDefault)
                    {
                        cust.DefaultEmailAddress = email;
                    }

                    // save changes
                    await unitOfWork.CompleteAsync();

                    tmr.Stop();
                    System.Diagnostics.Debug.WriteLine("VerifyEmailAddressByCustomerIdEmailIdToken took " + tmr.ElapsedMilliseconds + " ms");

                    return Ok(this.TheModelFactory.Create(email));
                }

                return BadRequest("Invalid verification token");
            }

            return NotFound();
        }

        private async Task<bool> SendEmailAddressAddedEmail(Guid custId, int emailId, Guid token, string userId, string siteCode, int languageId, string recipient, string sender)
        {
            var result = false;

            string confirmationToken = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(userId);

            var callbackUrl = new Uri(Url.Link("VerifyEmailAddressByCustomerIdEmailIdToken", new { custId = custId, emailId = emailId, token = token }));


            // use subject to specify the template and body to
            string subject = jCtrl.WebApi.Services.EmailService.EmailType.User_Verify_Email_Address.ToString();
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
                    values.Add("subject", "New Email Address Added EN");
                    values.Add("title", "New Email Address Added");
                    values.Add("body", "<p>A request to add your email address (" + recipient.ToLowerInvariant() + ") has been received from " + sender + "<br /><br />Please use the following link to confirm this request and verify your email address: <a href=\"" + callbackUrl + "\">Accept Request & Verify Address</a><br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 2: // FR
                    values.Add("language", "FR");
                    values.Add("subject", "New Email Address Added FR");
                    values.Add("title", "New Email Address Added");
                    values.Add("body", "<p>A request to add your email address (" + recipient.ToLowerInvariant() + ") has been received from " + sender + "<br /><br />Please use the following link to confirm this request and verify your email address: <a href=\"" + callbackUrl + "\">Accept Request & Verify Address</a><br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 3: // DE
                    values.Add("language", "DE");
                    values.Add("subject", "New Email Address Added DE");
                    values.Add("title", "New Email Address Added");
                    values.Add("body", "<p>A request to add your email address (" + recipient.ToLowerInvariant() + ") has been received from " + sender + "<br /><br />Please use the following link to confirm this request and verify your email address: <a href=\"" + callbackUrl + "\">Accept Request & Verify Address</a><br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 4: // NL
                    values.Add("language", "NL");
                    values.Add("subject", "New Email Address Added NL");
                    values.Add("title", "New Email Address Added");
                    values.Add("body", "<p>A request to add your email address (" + recipient.ToLowerInvariant() + ") has been received from " + sender + "<br /><br />Please use the following link to confirm this request and verify your email address: <a href=\"" + callbackUrl + "\">Accept Request & Verify Address</a><br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                case 5: // US
                    values.Add("language", "US");
                    values.Add("subject", "New Email Address Added US");
                    values.Add("title", "New Email Address Added");
                    values.Add("body", "<p>A request to add your email address (" + recipient.ToLowerInvariant() + ") has been received from " + sender + "<br /><br />Please use the following link to confirm this request and verify your email address: <a href=\"" + callbackUrl + "\">Accept Request & Verify Address</a><br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
                default:

                    values.Add("language", "EN");
                    values.Add("subject", "New Email Address Added DEF");
                    values.Add("title", "New Email Address Added");
                    values.Add("body", "<p>A request to add your email address (" + recipient.ToLowerInvariant() + ") has been received from " + sender + "<br /><br />Please use the following link to confirm this request and verify your email address: <a href=\"" + callbackUrl + "\">Accept Request & Verify Address</a><br /><br />SNG Barratt Jaguar Parts</p><br />");
                    break;
            }

            string body = JsonConvert.SerializeObject(values);

            try
            {

                //TODO: send via mandrill / mailchimp
                // AppUserManager and this should go via same service 
                // so easier to swap out
                // await this.AppUserManager.SendEmailAsync(userId, subject, body);

                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                throw ex;
            }

            return result;
        }

        #endregion

        #region "Linked User Accounts"

        [HttpGet]
        [Route("{custId:guid}/users", Name = "GetLinkedUsersByCustomerId")]
        [ResponseType(typeof(List<LinkedUserReturnModel>))]
        public async Task<IHttpActionResult> GetLinkedUsersByCustomerIdAsync([FromUri] Guid custId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var users = await unitOfWork.UserAccounts.GetUsers(custId);


                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetLinkedUsersByCustomerId took " + tmr.ElapsedMilliseconds + " ms");

                            return Ok(this.TheModelFactory.Create(users));

                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        [HttpPost]
        [Route("{custId:guid}/users", Name = "CreateLinkedUserByCustomerIdEmailAddress")]
        [ResponseType(typeof(List<LinkedUserReturnModel>))]
        public async Task<IHttpActionResult> CreateLinkedUserByCustomerIdEmailAddressAsync([FromUri] Guid custId, [FromBody] string email)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            if (ModelState.IsValid)
                            {
                                var tmr = new System.Diagnostics.Stopwatch();
                                tmr.Start();

                                var cust = await unitOfWork.Customers
                                    .SingleOrDefault(c => c.Id == custId);

                                if (cust != null)
                                {
                                    var linkUser = await unitOfWork.UserAccounts.GetUserByEmail(email.ToLowerInvariant());

                                    if (linkUser != null)
                                    {
                                        if (linkUser.Customer_Id == null || linkUser.Customer_Id == cust.Id)
                                        {
                                            // user is not already linked to a customer account
                                            // or is linked to this customer account

                                            if (linkUser.Customer_Id == null) linkUser.Customer_Id = cust.Id;

                                            await unitOfWork.CompleteAsync();

                                            tmr.Stop();
                                            System.Diagnostics.Debug.WriteLine("CreateLinkedUserByCustomerIdEmailAddress took " + tmr.ElapsedMilliseconds + " ms");

                                            return await GetLinkedUsersByCustomerIdAsync(custId);
                                        }

                                        return BadRequest("User already linked to a different account.");
                                    }

                                    return BadRequest("User not registered: " + email);
                                }

                                return BadRequest("Customer not found.");
                            }

                            return BadRequest(ModelState);
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/users/{userId:guid}", Name = "GetLinkedUserByCustomerIdUserId")]
        [ResponseType(typeof(LinkedUserReturnModel))]
        public async Task<IHttpActionResult> GetLinkedUserByCustomerIdUserIdAsync([FromUri] Guid custId, [FromUri] Guid userId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var account = await unitOfWork.UserAccounts.GetUserByIdCustomerId(userId, custId);

                            if (account != null)
                            {

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetLinkedUserByCustomerIdUserId took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(account));
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        [HttpDelete]
        [Route("{custId:guid}/users/{userId:guid}", Name = "DeleteLinkedUserByCustomerIdUserId")]
        [ResponseType(typeof(List<LinkedUserReturnModel>))]
        public async Task<IHttpActionResult> DeleteLinkedUserByCustomerIdUserIdAsync([FromUri] Guid custId, [FromUri] Guid userId)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {
                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var account = await unitOfWork.UserAccounts.GetUserByIdCustomerId(userId, custId);

                            if (account != null)
                            {
                                account.Customer_Id = null;

                                await unitOfWork.CompleteAsync();

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("DeleteLinkedUserByCustomerIdUserId took " + tmr.ElapsedMilliseconds + " ms");

                                return await GetLinkedUsersByCustomerIdAsync(custId);
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        #endregion


        #region "Shipping Addresses"

        [HttpGet]
        [Route("{custId:guid}/addresses", Name = "GetShippingAddressesByCustomerId")]
        [ResponseType(typeof(List<ShippingAddressReturnModel>))]
        public async Task<IHttpActionResult> GetShippingAddressesByCustomerIdAsync([FromUri] Guid custId)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {
                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var addresses = await unitOfWork.CustomerShippingAddresses.GetShippingAddressesByCustomer(custId);

                            //if (addresses.Any())
                            //{

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetShippingAddressesByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            return Ok(this.TheModelFactory.Create(addresses.ToList()));
                            //}

                            //return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        [HttpPost]
        [Route("{custId:guid}/addresses", Name = "CreateCustomerShippingAddress")]
        [ResponseType(typeof(ShippingAddressReturnModel))]
        public async Task<IHttpActionResult> CreateCustomerShippingAddressAsync([FromUri] Guid custId, [FromBody] CreateShippingAddressBindingModel address)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            if (ModelState.IsValid)
                            {
                                var tmr = new System.Diagnostics.Stopwatch();
                                tmr.Start();

                                var cust = await unitOfWork.Customers.GetCustomer(custId, false, false, c => c.ShippingAddresses);

                                if (cust != null)
                                {
                                    var addr = new CustomerShippingAddress
                                    {
                                        Customer_Id = custId,
                                        DisplayName = address.DisplayName,
                                        Name = address.Name,
                                        AddressLine1 = address.AddressLine1,
                                        AddressLine2 = address.AddressLine2,
                                        TownCity = address.TownCity,
                                        CountyState = address.CountyState,
                                        PostalCode = address.PostalCode,
                                        CountryName = address.Country,
                                        Country_Code = address.CountryCode,
                                        PhoneNumber = address.PhoneNumber,
                                        IsDefault = address.Default,
                                        RowVersion = 1,
                                        CreatedTimestampUtc = DateTime.UtcNow,
                                        CreatedByUsername = user.UserName,
                                        UpdatedTimestampUtc = DateTime.UtcNow,
                                        UpdatedByUsername = user.UserName

                                    };

                                    // TODO: validate the address ?

                                    try
                                    {
                                        cust.ShippingAddresses.Add(addr);

                                        await unitOfWork.CompleteAsync();

                                        if (address.Default == true)
                                        {
                                            cust.DefaultShippingAddress = addr;

                                            await unitOfWork.CompleteAsync();
                                        }

                                        return Ok(this.TheModelFactory.Create(addr));
                                    }
                                    catch (Exception e)
                                    {
                                        System.Diagnostics.Debug.WriteLine(e.Message);

                                        throw e;
                                    }
                                }

                                return BadRequest("Customer not found.");
                            }
                            return BadRequest(ModelState);
                        }
                        return Unauthorized();
                    }
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/addresses/{addressId:int}", Name = "GetShippingAddressByCustomerIdAddressId")]
        [ResponseType(typeof(ShippingAddressReturnModel))]
        public async Task<IHttpActionResult> GetShippingAddressByCustomerIdAddressIdAsync([FromUri] Guid custId, [FromUri] int addressId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var address = await unitOfWork.CustomerShippingAddresses.GetShippingAddressByCustomerIdAddressId(custId, addressId);

                            if (address != null)
                            {

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetShippingAddressByCustomerIdAddressId took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(address));
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{custId:guid}/addresses/{addressId:int}", Name = "UpdateCustomerShippingAddress")]
        [ResponseType(typeof(ShippingAddressReturnModel))]
        public async Task<IHttpActionResult> UpdateCustomerShippingAddressAsync([FromUri] Guid custId, [FromUri]int addressId, [FromBody] UpdateShippingAddressBindingModel address)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user =  await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            if (ModelState.IsValid)
                            {
                                var tmr = new System.Diagnostics.Stopwatch();
                                tmr.Start();

                                var cust = await unitOfWork.Customers.GetCustomer(custId, false, false, c => c.ShippingAddresses);

                                if (cust != null)
                                {
                                    var addr = await unitOfWork.CustomerShippingAddresses.GetShippingAddressByCustomerIdAddressId(custId, addressId);

                                    if (addr != null)
                                    {
                                        // update the address
                                        addr.DisplayName = address.DisplayName;
                                        addr.Name = address.Name;
                                        addr.AddressLine1 = address.AddressLine1;
                                        addr.AddressLine2 = address.AddressLine2;
                                        addr.TownCity = address.TownCity;
                                        addr.CountyState = address.CountyState;
                                        addr.PostalCode = address.PostalCode;
                                        addr.CountryName = address.Country;
                                        addr.Country_Code = address.CountryCode;
                                        addr.PhoneNumber = address.PhoneNumber;
                                        addr.IsDefault = address.Default;
                                        addr.RowVersion += 1;
                                        addr.UpdatedTimestampUtc = DateTime.UtcNow;
                                        addr.UpdatedByUsername = user.UserName;

                                        // TODO: validate the address ?

                                        try
                                        {


                                            await unitOfWork.CompleteAsync();

                                            if (addr.IsDefault == true)
                                            {
                                                cust.DefaultShippingAddress = addr;

                                                await unitOfWork.CompleteAsync();
                                            }

                                            tmr.Stop();
                                            System.Diagnostics.Debug.WriteLine("UpdateShippingAddressByCustomerIdAddressId took " + tmr.ElapsedMilliseconds + " ms");

                                            return Ok(this.TheModelFactory.Create(addr));

                                        }
                                        catch (Exception e)
                                        {
                                            System.Diagnostics.Debug.WriteLine(e.Message);

                                            throw e;
                                        }



                                    }

                                    return NotFound();

                                }

                                return BadRequest("Customer not found.");
                            }
                            return BadRequest(ModelState);
                        }
                        return Unauthorized();
                    }
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{custId:guid}/addresses/{addressId:int}", Name = "DeleteShippingAddressByCustomerIdAddressId")]
        [ResponseType(typeof(List<ShippingAddressReturnModel>))]
        public async Task<IHttpActionResult> DeleteShippingAddressByCustomerIdAddressIdAsync([FromUri] Guid custId, [FromUri] int addressId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var address = await unitOfWork.CustomerShippingAddresses.GetShippingAddressByCustomerIdAddressId(custId, addressId);

                            if (address != null)
                            {

                                unitOfWork.CustomerShippingAddresses.Remove(address);
                                await unitOfWork.CompleteAsync();

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("DeleteShippingAddressByCustomerIdAddressId took " + tmr.ElapsedMilliseconds + " ms");

                                return await GetShippingAddressesByCustomerIdAsync(custId);
                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        #endregion

        #region "Payment Options"

        [HttpGet]
        [Route("{custId:guid}/payments", Name = "GetCustomerPaymentOptions")]
        [ResponseType(typeof(PaymentOptionsReturnModel))]
        public async Task<IHttpActionResult> GetCustomerPaymentOptionsAsync([FromUri] Guid custId)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user =  await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {

                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            var cust = await unitOfWork.Customers.GetCustomer(custId, false, false, c => c.Branch, c => c.Branch.PaymentMethods);

                            if (cust != null)
                            {
                                // get payment methods for branch
                                var methods = await unitOfWork.BranchPaymentMethods.GetPaymentMethodsByBranch(cust.Branch_Id);

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetCartPaymentOptionsAsync took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(methods.ToList(), cust));

                            }

                            return NotFound();
                        }

                        return Unauthorized();
                    }
                }
            }

            return NotFound();
        }

        #endregion

        #region "Web Orders"        

        [HttpPost]
        [Route("{custId:guid}/orders", Name = "CreateCustomerWebOrder")]
        [ResponseType(typeof(WebOrderReturnModel))]
        public async Task<IHttpActionResult> CreateCustomerWebOrderAsync([FromUri] Guid custId, [FromBody] CreateWebOrderBindingModel order)
        {

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            if (ModelState.IsValid)
                            {
                                var tmr = new System.Diagnostics.Stopwatch();
                                tmr.Start();

                                var cust = await unitOfWork.Customers.GetCustomer(custId, false, false,
                                    c => c.Branch.TaxRates,
                                    c => c.ShippingAddresses,
                                    c => c.PaymentCards,
                                    c => c.TradingTerms);

                                if (cust != null)
                                {
                                    // TODO: check same branch as checkout

                                    #region "Order"

                                    var ord = new WebOrder
                                    {
                                        Customer_Id = custId,
                                        Customer = cust,
                                        CustomerTaxNo = cust.CompanyTaxNo,
                                        InternalCustNo = cust.AccountNumber,
                                        Language_Id = cust.Language_Id,
                                        Branch_Id = cust.Branch_Id,
                                        Branch = cust.Branch,
                                        BillingName = cust.AccountName,
                                        BillingAddressLine1 = cust.AddressLine1,
                                        BillingAddressLine2 = cust.AddressLine2,
                                        BillingTownCity = cust.CountyState,
                                        BillingCountyState = cust.CountyState,
                                        BillingPostalCode = cust.PostalCode,
                                        BillingCountryName = cust.CountryName,
                                        BillingCountryCode = cust.Country_Code,
                                        Items = new List<WebOrderItem>(),
                                        CustomerConfirmationRequired = false,
                                        RowVersion = 1,
                                        CreatedTimestampUtc = DateTime.UtcNow,
                                        CreatedByUsername = user.UserName,
                                        UpdatedTimestampUtc = DateTime.UtcNow,
                                        UpdatedByUsername = user.UserName

                                    };

                                    #endregion

                                    var estWeightKgs = 0m;

                                    #region "Items"

                                    if (order.Items != null)
                                    {
                                        decimal width, height, depth, weight;
                                        short lineNo = 1;
                                        CartItem itm;
                                        foreach (var checkoutItem in order.Items)
                                        {
                                            // get cart item from db
                                            itm = await unitOfWork.CartItems.GetCartItem(checkoutItem.CartItemId);

                                            if (itm != null)
                                            {
                                                // TODO: check cart item is still valid

                                                width = itm.BranchProduct.ProductDetails.ItemWidthCms;
                                                if (itm.BranchProduct.ProductDetails.PackedWidthCms > 0)
                                                {
                                                    width = itm.BranchProduct.ProductDetails.PackedWidthCms;
                                                }

                                                height = itm.BranchProduct.ProductDetails.ItemHeightCms;
                                                if (itm.BranchProduct.ProductDetails.PackedHeightCms > 0)
                                                {
                                                    height = itm.BranchProduct.ProductDetails.PackedHeightCms;
                                                }

                                                depth = itm.BranchProduct.ProductDetails.ItemDepthCms;
                                                if (itm.BranchProduct.ProductDetails.PackedDepthCms > 0)
                                                {
                                                    depth = itm.BranchProduct.ProductDetails.PackedDepthCms;
                                                }

                                                weight = itm.BranchProduct.ProductDetails.ItemWeightKgs;
                                                if (itm.BranchProduct.ProductDetails.PackedWeightKgs > 0)
                                                {
                                                    weight = itm.BranchProduct.ProductDetails.PackedWeightKgs;
                                                }

                                                ord.Items.Add(new WebOrderItem()
                                                {
                                                    Branch_Id = cust.Branch_Id,
                                                    Branch = cust.Branch,
                                                    BranchProduct_Id = itm.BranchProduct_Id,
                                                    BranchProduct = itm.BranchProduct,
                                                    Customer_Id = cust.Id,
                                                    Customer = cust,
                                                    CustomerLevel_Id = cust.TradingTerms_Code,
                                                    CustomerLevel = cust.TradingTerms,
                                                    DiscountCode = itm.DiscountCode,
                                                    PartNumber = itm.PartNumber,
                                                    PartTitle = itm.PartTitle,
                                                    Product_Id = itm.BranchProduct.Product_Id,
                                                    ProductDetails = itm.BranchProduct.ProductDetails,
                                                    PackedWidthCms = width,
                                                    PackedHeightCms = height,
                                                    PackedDepthCms = depth,
                                                    PackedWeightKgs = weight,
                                                    RetailPrice = itm.RetailPrice,
                                                    Surcharge = itm.Surcharge,
                                                    UnitPrice = itm.UnitPrice,
                                                    TaxRateCategory_Id = itm.BranchProduct.ProductDetails.TaxRateCategory_Id,
                                                    QuantityRequired = itm.QuantityRequired,
                                                    QuantityAllocated = 0,
                                                    QuantityBackOrdered = 0,
                                                    QuantityPicked = 0,
                                                    QuantityPacked = 0,
                                                    QuantityInvoiced = 0,
                                                    QuantityCredited = 0,
                                                    WebOrderItemStatus_Id = "S", // request submitted
                                                    LineNo = lineNo,
                                                    RowVersion = 1,
                                                    CreatedTimestampUtc = DateTime.UtcNow,
                                                    CreatedByUsername = user.UserName,
                                                    UpdatedTimestampUtc = DateTime.UtcNow,
                                                    UpdatedByUsername = user.UserName
                                                });

                                                // add weight to running total
                                                estWeightKgs += weight * itm.QuantityRequired;

                                                lineNo += 1;
                                            }
                                            else
                                            {
                                                // cart item not found                                                    
                                                throw new Exception("Cart item not found: " + checkoutItem.CartItemId);
                                            }
                                        }
                                    }



                                    #endregion

                                    #region "Recipient"

                                    if (order.Recipient != null)
                                    {
                                        ord.DeliveryName = order.Recipient.Name;
                                        ord.DeliveryAddressLine1 = order.Recipient.AddressLine1;
                                        ord.DeliveryAddressLine2 = order.Recipient.AddressLine2;
                                        ord.DeliveryTownCity = order.Recipient.TownCity;
                                        ord.DeliveryPostalCode = order.Recipient.PostalCode;
                                        ord.DeliveryCountyState = order.Recipient.CountyState;
                                        ord.DeliveryCountryName = order.Recipient.Country;
                                        ord.DeliveryCountryCode = order.Recipient.CountryCode;
                                        ord.DeliveryContactNumber = order.Recipient.PhoneNumber;
                                    }
                                    else if (order.Collect == true)
                                    {
                                        // branch
                                        ord.DeliveryName = cust.AccountName;
                                        ord.DeliveryAddressLine1 = cust.Branch.AddressLine1;
                                        ord.DeliveryAddressLine2 = cust.Branch.AddressLine2;
                                        ord.DeliveryTownCity = cust.Branch.TownCity;
                                        ord.DeliveryPostalCode = cust.Branch.PostalCode;
                                        ord.DeliveryCountyState = cust.Branch.CountyState;
                                        ord.DeliveryCountryName = cust.Branch.CountryName;
                                        ord.DeliveryCountryCode = cust.Branch.Country_Code;
                                        ord.DeliveryContactNumber = cust.DefaultPhoneNumber.FullNumber();
                                    }

                                    #endregion

                                    #region "Shipping"

                                    if (order.ShippingId != null)
                                    {
                                        // get shipping quote from db
                                        var qte = await unitOfWork.ShippingQuotes.GetShippingQuote(order.ShippingId);

                                        if (qte != null)
                                        {
                                            if (qte.Customer_Id == cust.Id)
                                            {
                                                if (qte.Branch_Id == cust.Branch_Id)
                                                {
                                                    ord.EstimatedShippingCost = qte.CostPrice;
                                                    ord.ShippingCharge = qte.Price;
                                                    ord.ShippingMethodName = qte.ShippingMethod.Title;
                                                    ord.ShippingMethod_Id = qte.ShippingMethod_Id;

                                                }
                                                else
                                                {
                                                    // wrong branch
                                                    throw new Exception("Shipping quote belongs to different branch");
                                                }
                                            }
                                            else
                                            {
                                                // wrong customer
                                                throw new Exception("Shipping quote belongs to different customer");
                                            }
                                        }
                                        else
                                        {
                                            // shipping quote not found
                                            throw new Exception("Shipping quote not found");
                                        }
                                    }
                                    else if (order.Quote == true)
                                    {
                                        ord.CustomerConfirmationRequired = true;
                                    }

                                    ord.EstimatedShippingWeightKgs = estWeightKgs;

                                    //if (order.Packages != null)
                                    //{
                                    //    ord.EstimatedShippingWeightKgs = order.Packages.estWeight;
                                    //}

                                    // remove all shipping quotes for customer
                                    foreach (var qte in await unitOfWork.ShippingQuotes.GetShippingQuotesByCustomer(cust.Id))
                                    {
                                        unitOfWork.ShippingQuotes.Remove(qte);
                                    }


                                    #endregion

                                    if (order.VoucherCode != null)
                                    {
                                        // get voucher from db and check still applies

                                        var voucher = await unitOfWork.Vouchers.GetVoucher(order.VoucherCode);

                                        if (voucher != null)
                                        {

                                        }
                                        else
                                        {
                                            // voucher not found
                                            throw new Exception("Discount voucher not found");
                                        }

                                    }


                                    try
                                    {
                                        cust.WebOrders.Add(ord);

                                        await unitOfWork.CompleteAsync();

                                        // TODO: send confirmation email

                                        return Ok(this.TheModelFactory.Create(ord));
                                    }
                                    catch (Exception e)
                                    {
                                        System.Diagnostics.Debug.WriteLine(e.Message);

                                        throw e;
                                    }



                                }

                                return BadRequest("Customer not found.");
                            }
                            return BadRequest(ModelState);
                        }
                        return Unauthorized();
                    }
                }
            }


            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/orders", Name = "GetWebOrdersByCustomerId")]
        [ResponseType(typeof(PagedResultsReturnModel<WebOrderListReturnModel>))]
        public async Task<IHttpActionResult> GetWebOrderListByCustomerIdAsync([FromUri] Guid custId, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {
            if (pageSize > 100) return BadRequest("Max page size 100");

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            IQueryable<WebOrder> query = unitOfWork.WebOrders.GetOrderByCustomerId(custId);

                            var totalCount = query.Count();

                            var pgSkip = (int)(pageSize * (pageNo - 1));
                            var pgTake = (int)(pageSize);

                            var results = this.TheModelFactory.Create(
                                await query
                                    .OrderBy(o => o.OrderDate)
                                    .ThenBy(o => o.OrderNo)
                                    .Skip(() => pgSkip) // use lamdas to force paramatisation
                                    .Take(() => pgTake)
                                    .Cacheable()
                                    .ToListAsync()
                                );


                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetWebOrderListByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
                            if (totalPages < 1) { totalPages = 1; }

                            var urlHelper = new UrlHelper(Request);
                            var prevLink = pageNo > 1 ? urlHelper.Link("GetWebOrderListByCustomerId", new { pageNo = pageNo - 1, pageSize = pageSize }) : "";
                            var nextLink = pageNo < totalPages ? urlHelper.Link("GetWebOrderListByCustomerId", new { pageNo = pageNo + 1, pageSize = pageSize }) : "";


                            return Ok(new PagedResultsReturnModel<WebOrderListReturnModel>
                            {
                                PageSize = pageSize,
                                PageCount = totalPages,
                                PageNo = pageNo,
                                ResultsCount = totalCount,
                                Results = results,
                                PrevPageUrl = prevLink,
                                NextPageUrl = nextLink
                                //LoadPageUrl = pageLink,
                            });
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        [HttpGet]
        [Route("{custId:guid}/orders/current", Name = "GetCurrentWebOrdersByCustomerId")]
        [ResponseType(typeof(PagedResultsReturnModel<WebOrderListReturnModel>))]
        public async Task<IHttpActionResult> GetCurrentWebOrdersByCustomerIdAsync([FromUri] Guid custId, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {
            if (pageSize > 100) return BadRequest("Max page size 100");

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            IQueryable<WebOrder> query = unitOfWork.WebOrders.GetCurrentOrdersByCustomerId(custId, true);

                            var totalCount = query.Count();

                            var pgSkip = (int)(pageSize * (pageNo - 1));
                            var pgTake = (int)(pageSize);

                            var results = this.TheModelFactory.Create(
                                await query
                                    .OrderBy(o => o.OrderDate)
                                    .ThenBy(o => o.OrderNo)
                                    .Skip(() => pgSkip) // use lamdas to force paramatisation
                                    .Take(() => pgTake)
                                    .Cacheable()
                                    .ToListAsync()
                                );

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetActiveWebOrdersByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
                            if (totalPages < 1) { totalPages = 1; }

                            var urlHelper = new UrlHelper(Request);
                            var prevLink = pageNo > 1 ? urlHelper.Link("GetActiveWebOrdersByCustomerId", new { pageNo = pageNo - 1, pageSize = pageSize }) : "";
                            var nextLink = pageNo < totalPages ? urlHelper.Link("GetActiveWebOrdersByCustomerId", new { pageNo = pageNo + 1, pageSize = pageSize }) : "";


                            return Ok(new PagedResultsReturnModel<WebOrderListReturnModel>
                            {
                                PageSize = pageSize,
                                PageCount = totalPages,
                                PageNo = pageNo,
                                ResultsCount = totalCount,
                                Results = results,
                                PrevPageUrl = prevLink,
                                NextPageUrl = nextLink
                                //LoadPageUrl = pageLink,
                            });
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/orders/history", Name = "GetHistoricWebOrdersByCustomerId")]
        [ResponseType(typeof(PagedResultsReturnModel<WebOrderListReturnModel>))]
        public async Task<IHttpActionResult> GetHistoricWebOrdersByCustomerIdAsync([FromUri] Guid custId, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {
            if (pageSize > 100) return BadRequest("Max page size 100");

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            IQueryable<WebOrder> query = unitOfWork.WebOrders.GetCurrentOrdersByCustomerId(custId, false);

                            var totalCount = query.Count();

                            var pgSkip = (int)(pageSize * (pageNo - 1));
                            var pgTake = (int)(pageSize);

                            var results = this.TheModelFactory.Create(
                                await query
                                    .OrderByDescending(o => o.OrderDate)
                                    .ThenByDescending(o => o.OrderNo)
                                    .Skip(() => pgSkip) // use lamdas to force paramatisation
                                    .Take(() => pgTake)
                                    .Cacheable()
                                    .ToListAsync()
                                );

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetHistoricWebOrdersByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
                            if (totalPages < 1) { totalPages = 1; }

                            var urlHelper = new UrlHelper(Request);
                            var prevLink = pageNo > 1 ? urlHelper.Link("GetHistoricWebOrdersByCustomerId", new { pageNo = pageNo - 1, pageSize = pageSize }) : "";
                            var nextLink = pageNo < totalPages ? urlHelper.Link("GetHistoricWebOrdersByCustomerId", new { pageNo = pageNo + 1, pageSize = pageSize }) : "";


                            return Ok(new PagedResultsReturnModel<WebOrderListReturnModel>
                            {
                                PageSize = pageSize,
                                PageCount = totalPages,
                                PageNo = pageNo,
                                ResultsCount = totalCount,
                                Results = results,
                                PrevPageUrl = prevLink,
                                NextPageUrl = nextLink
                                //LoadPageUrl = pageLink,
                            });
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();
        }

        [HttpGet]
        [Route("{custId:guid}/orders/{orderId:guid}", Name = "GetCustomerWebOrderByCustomerIdOrderId")]
        [ResponseType(typeof(WebOrderReturnModel))]
        public async Task<IHttpActionResult> GetCustomerWebOrderByCustomerIdOrderIdAsync([FromUri] Guid custId, [FromUri] Guid orderId)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {

                        // restrict access to self and account admins
                        if (user.Customer_Id == custId || User.IsInRole("system_admin") || User.IsInRole("accounts_author") || User.IsInRole("accounts_reviewer"))
                        {
                            var tmr = new System.Diagnostics.Stopwatch();
                            tmr.Start();

                            try
                            {
                                var order = await unitOfWork.WebOrders.GetOrder(orderId);

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetWebOrderByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                                if (order != null)
                                {
                                    return Ok(this.TheModelFactory.Create(order));
                                }
                                else
                                {
                                    return NotFound();
                                }

                            }
                            catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine(e.Message);

                                while (e.InnerException != null)
                                {
                                    e = e.InnerException;
                                    System.Diagnostics.Debug.WriteLine("Inner Exception:");
                                    System.Diagnostics.Debug.WriteLine(e.Message);
                                }

                                return BadRequest();
                            }
                        }

                        return Unauthorized();
                    }
                }
            }


            return NotFound();

        }

        #endregion
    }
}
