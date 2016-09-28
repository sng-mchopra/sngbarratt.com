using jCtrl.Services;
using jCtrl.Services.Core.Domain.WishList;
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
    [RoutePrefix("customers/{custId:guid}/lists")]
    public class WishListsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;


        public WishListsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("", Name = "GetWishListsByCustomerId")]
        [ResponseType(typeof(List<WishListReturnModel>))]
        public async Task<IHttpActionResult> GetWishListsByCustomerIdAsync([FromUri] Guid custId)
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

                            var cust = await unitOfWork.Customers.GetCustomer(custId);

                            if (cust != null)
                            {
                                var lists = await unitOfWork.WishLists.GetWishListsByCustomer(custId);

                                tmr.Stop();
                                System.Diagnostics.Debug.WriteLine("GetWishListsByCustomerId took " + tmr.ElapsedMilliseconds + " ms");

                                var lst = new List<WishListReturnModel>();

                                foreach (var list in lists)
                                {
                                    lst.Add(this.TheModelFactory.Create(cust, list));
                                }

                                return Ok(lst);
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
        [Route("", Name = "CreateWishList")]
        [ResponseType(typeof(WishListReturnModel))]
        public async Task<IHttpActionResult> CreateWishListAsync([FromUri] Guid custId, [FromBody] CreateWishListBindingModel WishList)
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

                                var cust = await unitOfWork.Customers.GetCustomer(custId);

                                if (cust != null)
                                {

                                    var list = new WishList()
                                    {
                                        Customer_Id = custId,
                                        Customer = cust,
                                        DisplayName = WishList.DisplayName,
                                        Items = new List<WishListItem>(),
                                        RowVersion = 1,
                                        CreatedByUsername = user.UserName,
                                        CreatedTimestampUtc = DateTime.UtcNow,
                                        UpdatedByUsername = user.UserName,
                                        UpdatedTimestampUtc = DateTime.UtcNow
                                    };

                                    // add items
                                    foreach (var item in WishList.Items)
                                    {
                                        var prod = await unitOfWork.Products
                                            .GetBranchProductByBranchIdProductId(cust.Branch_Id, item.ProductId);

                                        if (prod != null)
                                        {
                                            list.Items.Add(new WishListItem()
                                            {
                                                Customer_Id = custId,
                                                Customer = cust,
                                                Product_Id = prod.ProductDetails.Id,
                                                Product = prod.ProductDetails,
                                                PartNumber = prod.ProductDetails.PartNumber,
                                                PartTitle = item.Title,
                                                Quantity = item.Quantity,
                                                RowVersion = 1,
                                                CreatedByUsername = user.UserName,
                                                CreatedTimestampUtc = DateTime.UtcNow,
                                                UpdatedByUsername = user.UserName,
                                                UpdatedTimestampUtc = DateTime.UtcNow

                                            });
                                        }
                                    }

                                    unitOfWork.WishLists.Add(list);

                                    await unitOfWork.CompleteAsync();

                                    tmr.Stop();
                                    System.Diagnostics.Debug.WriteLine("CreateWishListItemAsync took " + tmr.ElapsedMilliseconds + " ms");

                                    return Ok(this.TheModelFactory.Create(cust, list));

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
        [Route("{listId:guid}", Name = "GetWishListById")]
        [ResponseType(typeof(WishListReturnModel))]
        public async Task<IHttpActionResult> GetWishListByIdAsync([FromUri] Guid custId, [FromUri] Guid listId)
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

                            var cust = await unitOfWork.Customers.GetCustomer(custId);

                            if (cust != null)
                            {

                                var list = await unitOfWork.WishLists.GetWishListsByCustomer(custId, listId);

                                if (list != null)
                                {
                                    tmr.Stop();
                                    System.Diagnostics.Debug.WriteLine("GetWishListAsync took " + tmr.ElapsedMilliseconds + " ms");

                                    return Ok(this.TheModelFactory.Create(cust, list));
                                }

                                return NotFound();

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
        [Route("{listId:guid}", Name = "DeleteWishListById")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> DeleteWishListByIdAsync([FromUri] Guid custId, [FromUri] Guid listId)
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

                            var cust = await unitOfWork.Customers.GetCustomer(custId);

                            if (cust != null)
                            {

                                var list = await unitOfWork.WishLists.GetWishListsByCustomer(custId, listId);

                                if (list != null)
                                {
                                    unitOfWork.WishListItems.RemoveRange(list.Items);
                                    unitOfWork.WishLists.Remove(list);

                                    await unitOfWork.CompleteAsync();

                                    tmr.Stop();
                                    System.Diagnostics.Debug.WriteLine("DeleteWishListAsync took " + tmr.ElapsedMilliseconds + " ms");

                                    return Ok(true);
                                }

                                return NotFound();
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
        [Route("{listId:guid}/item", Name = "CreateWishListItem")]
        [ResponseType(typeof(WishListReturnModel))]
        public async Task<IHttpActionResult> CreateWishListItemAsync([FromUri] Guid custId, [FromUri] Guid listId, [FromBody] CreateWishListItemBindingModel WishListItem)
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

                                var cust = await unitOfWork.Customers.GetCustomer(custId);

                                if (cust != null)
                                {
                                    var list = await unitOfWork.WishLists.GetWishListsByCustomer(custId, listId);

                                    if (list != null)
                                    {
                                        var prod = await unitOfWork.Products
                                            .GetBranchProductByBranchIdProductId(cust.Branch_Id, WishListItem.ProductId);

                                        if (prod != null)
                                        {
                                            // add item
                                            list.Items.Add(new WishListItem()
                                            {
                                                Customer_Id = custId,
                                                Customer = cust,
                                                WishList_Id = listId,
                                                Product_Id = prod.Product_Id,
                                                Product = prod.ProductDetails,
                                                PartNumber = prod.ProductDetails.PartNumber,
                                                PartTitle = WishListItem.Title,
                                                Quantity = WishListItem.Quantity,
                                                RowVersion = 1,
                                                CreatedByUsername = user.UserName,
                                                CreatedTimestampUtc = DateTime.UtcNow,
                                                UpdatedByUsername = user.UserName,
                                                UpdatedTimestampUtc = DateTime.UtcNow
                                            });

                                            await unitOfWork.CompleteAsync();

                                            tmr.Stop();
                                            System.Diagnostics.Debug.WriteLine("CreateWishListItemAsync took " + tmr.ElapsedMilliseconds + " ms");

                                            return await GetWishListByIdAsync(custId, listId);

                                        }

                                        return BadRequest("Product not found.");
                                    }

                                    return BadRequest("List not found.");
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
        [Route("{listId:guid}/item/{itemId:guid}", Name = "UpdateWishListItem")]
        [ResponseType(typeof(WishListReturnModel))]
        public async Task<IHttpActionResult> UpdateWishListItemAsync([FromUri] Guid custId, [FromUri] Guid listId, [FromUri] Guid itemId, [FromBody] UpdateWishListItemBindingModel WishListItem)
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

                                var cust = await unitOfWork.Customers.GetCustomer(custId);

                                if (cust != null)
                                {
                                    // get WishList item
                                    var item = await unitOfWork.WishListItems.GetWishListItem(itemId, custId, listId);

                                    if (item != null)
                                    {
                                        // update the existing item
                                        item.Quantity = WishListItem.Quantity;
                                        item.RowVersion += 1;
                                        item.UpdatedByUsername = user.UserName;
                                        item.UpdatedTimestampUtc = DateTime.UtcNow;

                                        await unitOfWork.CompleteAsync();

                                        tmr.Stop();
                                        System.Diagnostics.Debug.WriteLine("UpdateWishListItemAsync took " + tmr.ElapsedMilliseconds + " ms");

                                        return await GetWishListByIdAsync(custId, listId);
                                    }

                                    return NotFound();
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
        [Route("{listId:guid}/item/{itemId:guid}", Name = "DeleteWishListItem")]
        [ResponseType(typeof(WishListReturnModel))]
        public async Task<IHttpActionResult> DeleteWishListItemAsync([FromUri] Guid custId, [FromUri] Guid listId, [FromUri] Guid itemId)
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

                            var cust = await unitOfWork.Customers.GetCustomer(custId);

                            if (cust != null)
                            {
                                // get WishList item
                                var item = await unitOfWork.WishListItems.GetWishListItem(itemId, custId, listId);

                                if (item != null)
                                {
                                    unitOfWork.WishListItems.Remove(item);
                                    await unitOfWork.CompleteAsync();

                                    return await GetWishListByIdAsync(custId, listId);
                                }
                                return NotFound();
                            }
                        }
                        return Unauthorized();
                    }
                }
            }
            return NotFound();
        }
    }
}
