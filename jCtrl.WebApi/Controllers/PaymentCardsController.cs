using jCtrl.Services;
using jCtrl.Services.Core.Domain.Payment;
using jCtrl.WebApi.Models.Binding;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace jCtrl.WebApi.Controllers
{
    [Authorize]
    public class PaymentCardsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public PaymentCardsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous] // to facilitate guest checkout
        [HttpPost]
        [Route("cards/verify", Name = "VerifyPaymentCard")]
        public IHttpActionResult VerifyPaymentCardAsync(VerifyPaymentCardBindingModel cardDetails)
        {
            if (ModelState.IsValid)
            {
                // check card number passes luhn formula
                // check exp date is in future
                // check card scheme is accepted
                // check start date, issue number are present when required

                var result = jCtrl.Validation.PaymentCard.Validate(cardDetails.CardNumber, cardDetails.ExpiryMonth, cardDetails.ExpiryYear, cardDetails.StartMonth, cardDetails.StartYear, cardDetails.IssueNumber);

                if (!result.IsValid)
                {
                    return BadRequest(result.Message);
                }

                return Ok();
            }

            return BadRequest(ModelState);
        }

        #region "Customer Payment Cards"
        [HttpGet]
        [Route("customers/{custId:guid}/cards", Name = "GetPaymentCards")]
        [ResponseType(typeof(List<PaymentCardReturnModel>))]
        public async Task<IHttpActionResult> GetPaymentCardsAsync([FromUri]Guid custId)
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

                            var cards = await unitOfWork.PaymentCards.GetPaymentCards(custId);

                            //if (cards.Any())
                            //{

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("GetPaymentCardsByCustomerIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                            return Ok(this.TheModelFactory.Create(cards.ToList()));
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
        [Route("customers/{custId:guid}/cards", Name = "CreatePaymentCard")]
        [ResponseType(typeof(PaymentCardReturnModel))]
        public async Task<IHttpActionResult> CreatePaymentCardAsync([FromUri]Guid custId, CreatePaymentCardBindingModel cardDetails)
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


                                var cust = await unitOfWork.PaymentCards.GetCustomerByPaymentCard(custId);

                                if (cust != null)
                                {
                                    var card = new PaymentCard
                                    {
                                        Customer_Id = custId,
                                        DisplayName = cardDetails.DisplayName,
                                        CardNumber = cardDetails.CardNumber,
                                        ExpiryDate = new DateTime(cardDetails.ExpiryYear, cardDetails.ExpiryMonth, 1).AddMonths(1).AddDays(-1),
                                        IssueNumber = cardDetails.IssueNumber,
                                        IsDefault = cardDetails.Default,
                                        RowVersion = 1,
                                        CreatedTimestampUtc = DateTime.UtcNow,
                                        UpdatedTimestampUtc = DateTime.UtcNow
                                    };

                                    if (cardDetails.StartMonth != null && cardDetails.StartYear != null)
                                    {
                                        card.StartDate = new DateTime((int)cardDetails.StartYear, (int)cardDetails.StartMonth, 1);
                                    }

                                    var key = ConfigurationManager.AppSettings["Encryption_Key"];
                                    var salt = ConfigurationManager.AppSettings["Encryption_Salt"];

                                    // encrypt card details
                                    card.Encrypt(key, salt);


                                    try
                                    {

                                        unitOfWork.PaymentCards.Add(card);

                                        await unitOfWork.CompleteAsync();

                                        if (cardDetails.Default == true)
                                        {
                                            cust.DefaultPaymentCard = card;

                                            await unitOfWork.CompleteAsync();
                                        }


                                        return Ok(this.TheModelFactory.Create(card));
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
                                        // var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                                        return BadRequest(fullErrorMessage);

                                        // Throw a new DbEntityValidationException with the improved exception message.
                                        // throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                                    }
                                    catch (Exception e)
                                    {
                                        Trace.WriteLine(e.Message);

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
        [Route("customers/{custId:guid}/cards/{cardId:int}", Name = "GetPaymentCard")]
        [ResponseType(typeof(PaymentCardReturnModel))]
        public async Task<IHttpActionResult> GetPaymentCardAsync([FromUri]Guid custId, [FromUri]int cardId)
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
                                var card = await unitOfWork.PaymentCards.GetPaymentCard(custId, cardId);

                                if (card != null) { return Ok(this.TheModelFactory.Create(card)); }

                                return BadRequest("Card not found.");
                            }
                            catch (Exception e)
                            {
                                Trace.WriteLine(e.Message);

                                throw e;
                            }
                        }
                        return Unauthorized();
                    }
                }
            }
            return NotFound();
        }

        [HttpPut]
        [Route("customers/{custId:guid}/cards/{cardId:int}", Name = "UpdatePaymentCard")]
        [ResponseType(typeof(PaymentCardReturnModel))]
        public async Task<IHttpActionResult> UpdatePaymentCardAsync([FromUri]Guid custId, [FromUri]int cardId, UpdatePaymentCardBindingModel cardDetails)
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

                                var cust = await unitOfWork.PaymentCards.GetCustomerByPaymentCard(custId);

                                if (cust != null)
                                {
                                    var card = await unitOfWork.PaymentCards.GetPaymentCard(custId, cardId);

                                    if (card != null)
                                    {
                                        var key = ConfigurationManager.AppSettings["Encryption_Key"];
                                        var salt = ConfigurationManager.AppSettings["Encryption_Salt"];

                                        // decrypt card details
                                        card.Decrypt(key, salt);

                                        // update details

                                        card.DisplayName = cardDetails.DisplayName;

                                        if (!cardDetails.CardNumber.Contains('x'))
                                        {
                                            // only update card number if it is not masked
                                            card.CardNumber = cardDetails.CardNumber;
                                        }

                                        card.ExpiryDate = new DateTime(cardDetails.ExpiryYear, cardDetails.ExpiryMonth, 1).AddMonths(1).AddDays(-1);
                                        card.IssueNumber = cardDetails.IssueNumber;
                                        card.IsDefault = cardDetails.Default;
                                        card.RowVersion += 1;
                                        card.UpdatedTimestampUtc = DateTime.UtcNow;

                                        card.StartDate = null;
                                        if (cardDetails.StartMonth != null && cardDetails.StartYear != null)
                                        {
                                            card.StartDate = new DateTime((int)cardDetails.StartYear, (int)cardDetails.StartMonth, 1);
                                        }

                                        // encrypt card details
                                        card.Encrypt(key, salt);

                                        try
                                        {
                                            await unitOfWork.CompleteAsync();

                                            if (cardDetails.Default == true)
                                            {
                                                cust.DefaultPaymentCard = card;

                                                await unitOfWork.CompleteAsync();
                                            }

                                            return Ok(this.TheModelFactory.Create(card));

                                        }
                                        catch (Exception e)
                                        {
                                            Trace.WriteLine(e.Message);

                                            throw e;
                                        }

                                    }

                                    return BadRequest("Card not found.");
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
        [Route("customers/{custId:guid}/cards/{cardId:int}", Name = "DeletePaymentCard")]
        [ResponseType(typeof(List<PaymentCardReturnModel>))]
        public async Task<IHttpActionResult> DeletePaymentCardAsync([FromUri]Guid custId, [FromUri]int cardId)
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

                            // get cart item
                            var card = await unitOfWork.PaymentCards.GetPaymentCard(custId, cardId);

                            if (card != null)
                            {
                                unitOfWork.PaymentCards.Remove(card);
                                await unitOfWork.CompleteAsync();
                            }

                            tmr.Stop();
                            System.Diagnostics.Debug.WriteLine("DeletePaymentCard took " + tmr.ElapsedMilliseconds + " ms");

                            return await GetPaymentCardsAsync(custId);
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
