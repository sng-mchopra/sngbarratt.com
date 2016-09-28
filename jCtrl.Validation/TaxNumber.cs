using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Validation
{
    public static class TaxNumber
    {
        public enum ValidationResult
        {
            Valid,
            Invalid,
            Unavailable
        }

        public static async Task<ValidationResult> Validate(string CountryCode, string TaxNumber, string Reference)
        {

            try
            {
            
            // remove country code from start of tax number
            if (TaxNumber.StartsWith(CountryCode)) { TaxNumber = TaxNumber.Substring(CountryCode.Length); }

            string data = "memberStateCode=" + CountryCode + "&number=" + TaxNumber + "&traderName=&traderStreet=&traderPostalCode=&traderCity=&requesterMemberStateCode=&requesterNumber=&action=check&check=Verify";            

            using (var client = new HttpClient())
            {
                // set referrer and user agent
                client.DefaultRequestHeaders.Referrer = new Uri(Properties.Settings.Default.TaxNumber_Referral_Url);                
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");

                var url = Properties.Settings.Default.TaxNumber_Request_Url;

                var response = await client.PostAsync(
                    url,
                    new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded")
                );


                if (response.IsSuccessStatusCode)
                {

                    var html = await response.Content.ReadAsStringAsync();

                    var parser = new HtmlParser();
                    var document = await parser.ParseAsync(html);

                    var valid = document.QuerySelector("span.validStyle");
                    if (valid != null)
                    {
                        if (valid.InnerHtml.ToUpper().StartsWith("YES"))
                        {
                            // success
                            return ValidationResult.Valid;
                        }
                    }

                    var invalid = document.QuerySelector("span.invalidStyle");
                    if (invalid != null)
                    {
                        if (invalid.InnerHtml.ToUpper().StartsWith("NO"))
                        {
                            return ValidationResult.Invalid;
                        }
                    }

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

            return ValidationResult.Unavailable;
        }
    }
}
