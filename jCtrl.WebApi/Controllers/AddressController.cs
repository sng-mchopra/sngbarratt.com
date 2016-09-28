
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using jCtrl.WebApi.Models.Return;
using System.Configuration;
using jCtrl.WebApi.Models.Binding;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("address")]
    public class AddressController : BaseApiController
    {
        [HttpPost]
        [Route("capture", Name = "GetAddressSuggestions")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> GetAddressSuggestionsAsync(AddressCaptureBindingModel capture)
        {

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var response = await jCtrl.Services.External.AddressChecker.CaptureAddress(ConfigurationManager.AppSettings["EverythingLocation_Key"], capture.Address, capture.Country);

            if (response != null)
            {
                if (response.Status == "OK")
                {
                    return Ok(response.Output);
                }
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetAddressSuggestionsAsync took " + tmr.ElapsedMilliseconds + " ms");

            return InternalServerError();
        }

        [HttpPost]
        [Route("capture/{idx:int}", Name = "GetAddressSuggestionDetails")]
        [ResponseType(typeof(List<AddressLookupReturnModel>))]
        public async Task<IHttpActionResult> GetAddressSuggestionDetailsAsync(AddressCaptureBindingModel capture, int idx)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var response = await jCtrl.Services.External.AddressChecker.CompleteAddress(ConfigurationManager.AppSettings["EverythingLocation_Key"], capture.Address, capture.Country, idx);

            if (response != null)
            {
                if (response.Status == "OK")
                {
                    //return Ok( JsonConvert.SerializeObject(response.Output));
                    return Ok(this.TheModelFactory.Create(response.Output));
                }
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetAddressSuggestionDetailsAsync took " + tmr.ElapsedMilliseconds + " ms");

            return InternalServerError();
        }

        [HttpPost]
        [Route("verify", Name = "GetVerifiedAddress")]
        [ResponseType(typeof(AddressLookupReturnModel))]
        public async Task<IHttpActionResult> GetVerifiedAddressAsync(AddressBindingModel address)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var response = await jCtrl.Services.External.AddressChecker.VerifyAddress(
                ConfigurationManager.AppSettings["EverythingLocation_Key"],
                new jCtrl.Services.External.AddressChecker.UnverifiedAddress()
                {
                    Address1 = address.AddressLine1,
                    Address2 = address.AddressLine2,
                    Locality = address.TownCity,
                    AdministrativeArea = address.CountyState,
                    PostalCode = address.PostalCode,
                    Country = address.CountryCode
                }
            );

            if (response != null)
            {
                if (response.Status == "OK")
                {
                    var addr = response.Output.FirstOrDefault();
                    if (addr != null)
                    {
                        return Ok(this.TheModelFactory.Create(addr));
                    }
                }
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetVerifiedAddressAsync took " + tmr.ElapsedMilliseconds + " ms");

            return InternalServerError();
        }

    }
}