using jCtrl.Services;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Shipping;
using jCtrl.Shipping;
using jCtrl.WebApi.Models.Binding;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace jCtrl.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("customers/{custId:guid}/carts")]
    public class CartsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public CartsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("", Name = "GetCart")]
        [ResponseType(typeof(CartReturnModel))]
        public async Task<IHttpActionResult> GetCartAsync([FromUri] Guid custId)
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

                            var cust = await unitOfWork.Customers.GetCustomerByShippingAddress(custId);

                            if (cust != null)
                            {
                                var cart = await unitOfWork.CartItems.GetCustomerCart(cust);

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetCartAsync took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(cust, cart.ToList()));
                            }

                            return NotFound();
                        }
                        return Unauthorized();
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("sync", Name = "SyncCartByBindingModel")]
        [ResponseType(typeof(CartReturnModel))]
        public async Task<IHttpActionResult> SyncCartByBindingModelAsync([FromUri] Guid custId, [FromBody] UpdateCartBindingModel clientCart)
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

                                var cust = await unitOfWork.Customers.GetCustomerByShippingAddress(custId);

                                if (cust != null)
                                {
                                    // get existing cart items
                                    var dbCart = await unitOfWork.CartItems.GetCustomerCart(cust, true);

                                    if (dbCart == null) { dbCart = new List<CartItem>(); }

                                    // remove any db cart items that been removed on the client                                        
                                    foreach (CartItem dbCartItem in dbCart)
                                    {
                                        var clientCartItem = clientCart.Items.Where(c => c.Id == dbCartItem.Id).FirstOrDefault();
                                        if (clientCartItem == null)
                                        {
                                            // not in client cart

                                            // remove item
                                            unitOfWork.CartItems.Remove(dbCartItem);
                                        }
                                    }

                                    bool found = false;
                                    foreach (UpdateCartItemBindingModel itm in clientCart.Items)
                                    {
                                        found = false;
                                        if (itm.Id != null)
                                        {
                                            // search for item in collection and update qty
                                            var dbCartItem = dbCart.Where(c => c.Id == itm.Id).FirstOrDefault();

                                            if (dbCartItem != null)
                                            {
                                                found = true;

                                                if (itm.Quantity == 0)
                                                {
                                                    // remove item
                                                    unitOfWork.CartItems.Remove(dbCartItem);
                                                }
                                                else if (dbCartItem.QuantityRequired != itm.Quantity)
                                                {
                                                    // update the existing item
                                                    dbCartItem.QuantityRequired = itm.Quantity;
                                                    dbCartItem.RowVersion += 1;
                                                    dbCartItem.UpdatedByUsername = user.UserName;
                                                    dbCartItem.UpdatedTimestampUtc = DateTime.UtcNow;
                                                }

                                            }

                                        }
                                        else
                                        {
                                            // search for matching branch product in cart                                                
                                            var dbCartItem = dbCart.Where(c => c.BranchProduct_Id == itm.ProductId).FirstOrDefault();

                                            if (dbCartItem != null)
                                            {
                                                found = true;

                                                if (itm.Quantity == 0)
                                                {
                                                    // remove item
                                                    unitOfWork.CartItems.Remove(dbCartItem);

                                                }
                                                else if (dbCartItem.QuantityRequired != itm.Quantity)
                                                {
                                                    // update the item
                                                    dbCartItem.QuantityRequired += itm.Quantity;
                                                    dbCartItem.RowVersion += 1;
                                                    dbCartItem.UpdatedByUsername = user.UserName;
                                                    dbCartItem.UpdatedTimestampUtc = DateTime.UtcNow;
                                                }
                                            }

                                        }


                                        if (!found && itm.Quantity > 0)
                                        {
                                            // add new item

                                            // get branch product
                                            var product = await unitOfWork.Products.GetBranchProductByProductId(itm.ProductId);

                                            if (product != null)
                                            {

                                                var expires = DateTime.Today.AddDays(30); // TODO: get from a setting

                                                // add new cart item
                                                var newCartItem = new CartItem()
                                                {
                                                    Customer_Id = custId,
                                                    CustomerLevel_Id = cust.TradingTerms_Code,
                                                    Branch_Id = product.Branch_Id,
                                                    BranchProduct_Id = product.Id,
                                                    PartNumber = product.ProductDetails.PartNumber,
                                                    PartTitle = itm.Title,
                                                    DiscountCode = product.ProductDetails.DiscountLevel_Code,
                                                    RetailPrice = product.RetailPrice,
                                                    Surcharge = product.Surcharge,
                                                    UnitPrice = product.UnitPrice(cust.TradingTerms_Code),
                                                    QuantityRequired = itm.Quantity,
                                                    IsCheckedOut = false,
                                                    IsExpired = false,
                                                    ExpiresUtc = expires,
                                                    RowVersion = 1,
                                                    CreatedTimestampUtc = DateTime.UtcNow,
                                                    CreatedByUsername = user.UserName,
                                                    UpdatedTimestampUtc = DateTime.UtcNow,
                                                    UpdatedByUsername = user.UserName
                                                };


                                                unitOfWork.CartItems.Add(newCartItem);
                                            }
                                            else
                                            {
                                                return BadRequest("Product not found.");
                                            }
                                        }

                                    }

                                    // TODO: what about expired client items, do we need to show them to the user ?

                                    // at the moment an expired db item would get added again if it was still in the client cart
                                    // but the pricing etc would get updated 

                                    await unitOfWork.CompleteAsync();

                                    var cart = await unitOfWork.CartItems.GetCustomerCart(cust);

                                    tmr.Stop();
                                    System.Diagnostics.Debug.WriteLine("SyncCartByBindingModelAsync took " + tmr.ElapsedMilliseconds + " ms");

                                    return Ok(this.TheModelFactory.Create(cust, cart.ToList()));
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

        [HttpPost]
        [Route("item", Name = "CreateCartItem")]
        [ResponseType(typeof(CartReturnModel))]
        public async Task<IHttpActionResult> CreateCartItemAsync([FromUri] Guid custId, [FromBody] CreateCartItemBindingModel cartItem)
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

                                var cust = await unitOfWork.Customers.GetCustomerByShippingAddress(custId);

                                if (cust != null)
                                {
                                    var language = cust.Language_Id;

                                    // get existing cart items
                                    var cartItems = await unitOfWork.CartItems.GetCustomerCart(cust, true);

                                    // check for item already in cart
                                    bool found = false;
                                    if (cartItems != null)
                                    {
                                        foreach (CartItem itm in cartItems)
                                        {
                                            if (itm.BranchProduct_Id == cartItem.ProductId)
                                            {
                                                found = true;

                                                itm.QuantityRequired += (int)cartItem.Quantity;
                                                itm.RowVersion += 1;
                                                itm.UpdatedTimestampUtc = DateTime.UtcNow;
                                                itm.UpdatedByUsername = user.UserName;

                                                break;
                                            }
                                        }
                                    }
                                    else { cartItems = new List<CartItem>(); }

                                    // add new item if not found
                                    if (!found)
                                    {
                                        var product = await unitOfWork.Products.GetBranchProductByProductId(cartItem.ProductId);

                                        if (product != null)
                                        {
                                            unitOfWork.CartItems.Add(new CartItem()
                                            {
                                                Customer_Id = custId,
                                                CustomerLevel_Id = cust.TradingTerms_Code,
                                                Branch_Id = product.Branch_Id,
                                                BranchProduct_Id = product.Id,
                                                PartNumber = product.ProductDetails.PartNumber,
                                                PartTitle = product.ProductDetails.TextInfo.Where(t => t.Language_Id == language).FirstOrDefault().ShortTitle,
                                                DiscountCode = product.ProductDetails.DiscountLevel_Code,
                                                RetailPrice = product.RetailPrice,
                                                Surcharge = product.Surcharge,
                                                UnitPrice = product.UnitPrice(cust.TradingTerms_Code),
                                                QuantityRequired = (int)cartItem.Quantity,
                                                IsCheckedOut = false,
                                                IsExpired = false,
                                                ExpiresUtc = DateTime.Today.AddDays(30), // TODO: get from a setting
                                                RowVersion = 1,
                                                CreatedTimestampUtc = DateTime.UtcNow,
                                                CreatedByUsername = user.UserName,
                                                UpdatedTimestampUtc = DateTime.UtcNow,
                                                UpdatedByUsername = user.UserName


                                            });
                                        }
                                        else
                                        {
                                            return BadRequest("Product not found.");
                                        }

                                    }

                                    await unitOfWork.CompleteAsync();

                                    var cart = await unitOfWork.CartItems.GetCustomerCart(cust);

                                    tmr.Stop();
                                    System.Diagnostics.Debug.WriteLine("CreateCartItemAsync took " + tmr.ElapsedMilliseconds + " ms");

                                    return Ok(this.TheModelFactory.Create(cust, cart.ToList()));


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

        [HttpPut]
        [Route("item/{itemId:guid}", Name = "UpdateCartItem")]
        [ResponseType(typeof(CartReturnModel))]
        public async Task<IHttpActionResult> UpdateCartItemAsync([FromUri] Guid custId, [FromUri] Guid itemId, [FromBody] UpdateCartItemBindingModel cartItem)
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

                                var cust = await unitOfWork.Customers.GetCustomerByShippingAddress(custId);

                                if (cust != null)
                                {

                                    // get cart item
                                    var dbCartItem = await unitOfWork.CartItems.GetCustomerCartItem(itemId, custId);

                                    if (dbCartItem != null)
                                    {
                                        // update the existing item
                                        dbCartItem.QuantityRequired = cartItem.Quantity;
                                        dbCartItem.RowVersion += 1;
                                        dbCartItem.UpdatedByUsername = user.UserName;
                                        dbCartItem.UpdatedTimestampUtc = DateTime.UtcNow;

                                        await unitOfWork.CompleteAsync();
                                    }

                                    var cart = await unitOfWork.CartItems.GetCustomerCart(cust);

                                    tmr.Stop();
                                    System.Diagnostics.Debug.WriteLine("UpdateCartItemAsync took " + tmr.ElapsedMilliseconds + " ms");

                                    return Ok(this.TheModelFactory.Create(cust, cart.ToList()));
                                }

                                return NotFound();
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
        [Route("item/{itemId:guid}", Name = "DeleteCartItem")]
        [ResponseType(typeof(CartReturnModel))]
        public async Task<IHttpActionResult> DeleteCartItemAsync([FromUri] Guid custId, [FromUri] Guid itemId)
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

                            var cust = await unitOfWork.Customers.GetCustomerByShippingAddress(custId);

                            if (cust != null)
                            {

                                // get cart item
                                var cartItem = await unitOfWork.CartItems.GetCustomerCartItem(itemId, custId);

                                if (cartItem != null)
                                {
                                    unitOfWork.CartItems.Remove(cartItem);
                                    await unitOfWork.CompleteAsync();
                                }

                                var cart = await unitOfWork.CartItems.GetCustomerCart(cust);

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("DeleteCartItemAsync took " + tmr.ElapsedMilliseconds + " ms");

                                return Ok(this.TheModelFactory.Create(cust, cart.ToList()));
                            }


                        }

                        return Unauthorized();
                    }


                }
            }


            return NotFound();
        }


        [HttpPost]
        [Route("shipping", Name = "GetCartShippingOptions")]
        [ResponseType(typeof(ShippingOptionsReturnModel))]
        public async Task<IHttpActionResult> GetCartShippingOptionsAsync([FromUri] Guid custId, [FromBody] ContactBindingModel recipient)
        {
            try
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

                                    var cust = await unitOfWork.Customers.GetCustomerByShippingAddress(custId);

                                    if (cust != null)
                                    {

                                        // get current cart items
                                        var cart = await unitOfWork.CartItems.GetCustomerCart(cust, true);
                                        if (cart != null)
                                        {

                                            // create recipient
                                            var deliveryContact = new Contact()
                                            {
                                                Name = recipient.Name,
                                                AddressLine1 = recipient.AddressLine1,
                                                AddressLine2 = recipient.AddressLine2,
                                                TownCity = recipient.TownCity,
                                                CountyState = recipient.CountyState,
                                                PostalCode = recipient.PostalCode,
                                                CountryName = recipient.Country,
                                                Country_Code = recipient.CountryCode,
                                                PhoneNumber = recipient.PhoneNumber
                                            };

                                            // get delivery country
                                            var deliveryCountry = await unitOfWork.Countries.GetDeliveryCountryByCode(recipient.CountryCode);

                                            // get branch
                                            var branch = cust.Branch;

                                            // get all shipping providers
                                            var providers = await unitOfWork.ShippingProviders.GetShippingProviders();

                                            // get all shipping methods for branch
                                            var methods = await unitOfWork.ShippingMethods.GetShippingMethodByBranch(branch.Id);

                                            // get packing containers for branch
                                            var boxes = await unitOfWork.PackingContainers.GetPackingContainersByBranch(branch.Id);

                                            // create request reference
                                            var reference = user.Email + " " + DateTime.UtcNow;

                                            var quotes = new List<ShippingQuote>(); await Rates.GetShippingOptions(branch, cust, deliveryContact, deliveryCountry, providers.ToList(), methods.ToList(), boxes.ToList(), cart.ToList(), reference);
                                            if (quotes.Any())
                                            {
                                                // save all shipping quotes to db

                                                // this allows the quote to be recalled at the next stage of the checkout process 
                                                // without revealing the sensitive information to the client
                                                // TODO: these will need to be cleared down on a regular basis
                                                unitOfWork.ShippingQuotes.AddRange(quotes);
                                                await unitOfWork.CompleteAsync();

                                            }


                                            return Ok(this.TheModelFactory.Create(deliveryContact, quotes));
                                        }

                                        return BadRequest("Cart not found.");
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
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
