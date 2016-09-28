using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.External
{
    public static class AddressChecker
    {
        //// This DOESNT Work as 400 Error is Thrown by API
        //// check service availabiity
        //public async Task<bool> IsServiceAvailable()
        //{

        //    bool result = false;

        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            var response = await client.GetAsync(_base_url);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                result = true;
        //            }
        //        }
        //    }
        //    catch (HttpRequestException hre)
        //    {
        //        Debug.WriteLine("ERROR");
        //        Debug.WriteLine(hre.Message);

        //        var ex = hre.InnerException;
        //        while (ex != null)
        //        {
        //            Debug.WriteLine("INNER EXCPETION");
        //            Debug.WriteLine(hre.Message);

        //            ex = ex.InnerException;
        //        }

        //        result = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("ERROR");
        //        Debug.WriteLine(ex.Message);

        //        while (ex.InnerException != null)
        //        {
        //            ex = ex.InnerException;

        //            Debug.WriteLine("INNER EXCPETION");
        //            Debug.WriteLine(ex.Message);
        //        }

        //        result = false;
        //    }

        //    return result;
        //}

        // search addresses

        public static async Task<CaptureResponse> CaptureAddress(string key, string query, string country)
        {
            CaptureResponse result = null;

            switch (country.ToUpper())
            {
                case "EL": // Greece
                    country = "GR";
                    break;
                case "IC": // Canary Islands
                    country = "ES";
                    break;
            }

            try
            {
                var url = Location.Properties.Settings.Default.EverythingLocation_Url + "/address/complete";

                var req = new CaptureRequest()
                {
                    lqtkey = key,
                    query = query,
                    country = country
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(
                        url,
                        new StringContent
                        (
                            JsonConvert.SerializeObject(req),
                            Encoding.UTF8,
                            "application/json"
                        )
                    );

                    if (response.IsSuccessStatusCode)
                    {

                        var byteArray = await response.Content.ReadAsByteArrayAsync();

                        result = JsonConvert.DeserializeObject<CaptureResponse>(
                            Encoding.UTF8.GetString(byteArray, 0, byteArray.Length)
                        );
                    }


                }

            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR");
                Debug.WriteLine(hre.Message);

                var ex = hre.InnerException;
                while (ex != null)
                {
                    Debug.WriteLine("INNER EXCPETION");
                    Debug.WriteLine(hre.Message);

                    ex = ex.InnerException;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR");
                Debug.WriteLine(ex.Message);

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                    Debug.WriteLine("INNER EXCPETION");
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        // get full details for a search address hit
        public static async Task<CompleteResponse> CompleteAddress(string key, string query, string country, int index)
        {
            CompleteResponse result = null;

            switch (country.ToUpper())
            {
                case "EL": // Greece
                    country = "GR";
                    break;
                case "IC": // Canary Islands
                    country = "ES";
                    break;
            }

            try
            {
                var url = Location.Properties.Settings.Default.EverythingLocation_Url + "/address/capture";

                var req = new CompleteRequest()
                {
                    lqtkey = key,
                    query = query,
                    country = country,
                    result = index
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(
                        url,
                        new StringContent
                        (
                            JsonConvert.SerializeObject(req),
                            Encoding.UTF8,
                            "application/json"
                        )
                    );

                    if (response.IsSuccessStatusCode)
                    {

                        var byteArray = await response.Content.ReadAsByteArrayAsync();

                        result = JsonConvert.DeserializeObject<CompleteResponse>(
                            Encoding.UTF8.GetString(byteArray, 0, byteArray.Length)
                        );


                    }


                }

            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR");
                Debug.WriteLine(hre.Message);

                var ex = hre.InnerException;
                while (ex != null)
                {
                    Debug.WriteLine("INNER EXCPETION");
                    Debug.WriteLine(hre.Message);

                    ex = ex.InnerException;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR");
                Debug.WriteLine(ex.Message);

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                    Debug.WriteLine("INNER EXCPETION");
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        // validate an address
        public static async Task<VerifyResponse> VerifyAddress(string key, UnverifiedAddress address)
        {
            VerifyResponse result = null;

            switch (address.Country.ToUpper())
            {
                case "EL": // Greece
                    address.Country = "GR";
                    break;
                case "IC": // Canary Islands
                    address.Country = "ES";
                    break;
            }

            try
            {
                var url = Location.Properties.Settings.Default.EverythingLocation_Url + "/address/verify";

                var req = new VerifyRequest()
                {
                    lqtkey = key,
                    input = new[] { address }
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(
                        url,
                        new StringContent
                        (
                            JsonConvert.SerializeObject(req),
                            Encoding.UTF8,
                            "application/json"
                        )
                    );

                    if (response.IsSuccessStatusCode)
                    {

                        var byteArray = await response.Content.ReadAsByteArrayAsync();

                        result = JsonConvert.DeserializeObject<VerifyResponse>(
                            Encoding.UTF8.GetString(byteArray, 0, byteArray.Length)
                        );
                    }


                }

            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR");
                Debug.WriteLine(hre.Message);

                var ex = hre.InnerException;
                while (ex != null)
                {
                    Debug.WriteLine("INNER EXCPETION");
                    Debug.WriteLine(hre.Message);

                    ex = ex.InnerException;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR");
                Debug.WriteLine(ex.Message);

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                    Debug.WriteLine("INNER EXCPETION");
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }



        public class UnverifiedAddress
        {
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Address4 { get; set; }
            public string Address5 { get; set; }
            public string Address6 { get; set; }
            public string Address7 { get; set; }
            public string Address8 { get; set; }
            public string Locality { get; set; }
            public string AdministrativeArea { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
        }

        public class VerifiedAddress
        {
            public string AQI { get; set; }
            public string AVC { get; set; }
            public string MatchRuleLabel { get; set; }


            public string Address { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Address4 { get; set; }
            public string Address5 { get; set; }
            public string Address6 { get; set; }
            public string Address7 { get; set; }
            public string Address8 { get; set; }

            public string DeliveryAddress { get; set; }
            public string DeliveryAddress1 { get; set; }
            public string DeliveryAddress2 { get; set; }
            public string DeliveryAddress3 { get; set; }
            public string DeliveryAddress4 { get; set; }
            public string DeliveryAddress5 { get; set; }
            public string DeliveryAddress6 { get; set; }
            public string DeliveryAddress7 { get; set; }
            public string DeliveryAddress8 { get; set; }

            public string CountryName { get; set; }

            [JsonProperty("ISO3166-2")]
            public string ISO3166_2 { get; set; }

            [JsonProperty("ISO3166-3")]
            public string ISO3166_3 { get; set; }

            [JsonProperty("ISO3166-N")]
            public string ISO3166_N { get; set; }

            public string SuperAdministrativeArea { get; set; }
            public string AdministrativeArea { get; set; }
            public string SubAdministrativeArea { get; set; }

            public string Locality { get; set; }
            public string DependentLocality { get; set; }
            public string DoubleDependentLocality { get; set; }


            public string Thoroughfare { get; set; }
            public string DependentThoroughfare { get; set; }

            public string PostalCode { get; set; }
            public string PostalCodePrimary { get; set; }
            public string PostalCodeSecondary { get; set; }

            public string Premise { get; set; }
            public string PremiseNumber { get; set; }

            public string Building { get; set; }
            public string SubBuilding { get; set; }

            public string Organization { get; set; }
            public string PostBox { get; set; }
            public string Unmatched { get; set; }

        }


        #region "Responses - Public"

        public abstract class BaseResponse
        {
            public string Status { get; set; }
        }

        public class CaptureResponse : BaseResponse
        {
            public IEnumerable<string> Output { get; set; }
        }

        public class CompleteResponse : BaseResponse
        {
            public VerifiedAddress Output { get; set; }
        }

        public class VerifyResponse : BaseResponse
        {
            public IEnumerable<VerifiedAddress> Output { get; set; }
        }

        #endregion


        #region "Requests - Internal"

        internal abstract class BaseRequest
        {
            public string lqtkey { get; set; }
        }

        internal class CaptureRequest : BaseRequest
        {
            public string country { get; set; }
            public string query { get; set; }
        }

        internal class CompleteRequest : CaptureRequest
        {
            public int result { get; set; }
        }

        internal class VerifyRequest : BaseRequest
        {
            public IEnumerable<UnverifiedAddress> input { get; set; }
        }




        #endregion


    }
}
