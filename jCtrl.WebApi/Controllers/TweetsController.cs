using jCtrl.Services;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Repositories;
using jCtrl.WebApi.Models;
using jCtrl.WebApi.Models.Return;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("tweets")]
    public class TweetsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public TweetsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        [Route("", Name = "GetTweets")]
        [ResponseType(typeof(List<TweetReturnModel>))]
        public async Task<IHttpActionResult> GetTweetsAsync(int limit = 3)
        {
            if (limit > 10) return BadRequest("Max result limit 10");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            if (unitOfWork.Tweets.isTweetsTenMinutesOld())
            {
                // refresh tweets from twitter api
                var tweetFeed = await GetTwitterFeed(10);

                if (tweetFeed != string.Empty)
                {
                    dynamic dynJson = JsonConvert.DeserializeObject(tweetFeed);

                    var i = 0;
                    foreach (var item in dynJson)
                    {
                        i++;

                        var tweet = await unitOfWork.Tweets.Get(i);

                        if (tweet != null)
                        {

                            tweet.Text = item.text.ToString();

                            string url = null;
                            var urls = item.entities.urls;
                            if (urls != null)
                            {
                                if (urls.Count > 0)
                                {
                                    url = urls[0].url;
                                }
                            }

                            tweet.LinkUrl = url;
                            tweet.CreatedTimestampUtc = DateTime.ParseExact(item.created_at.ToString(), "ddd MMM dd HH:mm:ss zzz yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            tweet.UpdatedTimestampUtc = DateTime.UtcNow;

                            if (tweet.Text.Contains("http://") || tweet.Text.Contains("https://"))
                            {
                                var idx = -1;
                                if (tweet.Text.Contains("http://"))
                                {
                                    idx = tweet.Text.IndexOf("http://");
                                }
                                else
                                {
                                    idx = tweet.Text.IndexOf("https://");
                                }

                                if (idx > 0)
                                {
                                    url = tweet.Text.Substring(idx).Trim();
                                    if (url.Contains(" "))
                                    {
                                        url = url.Substring(0, url.IndexOf(" ")).Trim();
                                    }

                                    if (tweet.LinkUrl == null) tweet.LinkUrl = url;

                                    tweet.Text = tweet.Text.Substring(0, idx).Trim(); ;
                                }
                            }
                        }
                    }

                    await unitOfWork.CompleteAsync();
                }
            }

            IQueryable<Tweet> query = unitOfWork.Tweets.GetTweets();

            var lst = new List<TweetReturnModel>();

            foreach (Tweet tweet in await query
                .Take(limit)
                .ToListAsync()
                )
            {
                lst.Add(this.TheModelFactory.Create(tweet));
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetTweetsAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(lst);

        }

        private static async Task<String> GetTwitterFeed(int limit)
        {
            //oauth application keys            
            string oauth_token = ConfigurationManager.AppSettings["Twitter_Token"];
            string oauth_token_secret = ConfigurationManager.AppSettings["Twitter_Token_Secret"];
            string oauth_consumer_key = ConfigurationManager.AppSettings["Twitter_Consumer_Key"];
            string oauth_consumer_secret = ConfigurationManager.AppSettings["Twitter_Consumer_Secret"];

            // oauth implementation details
            string oauth_version = "1.0";
            string oauth_signature_method = "HMAC-SHA1";

            // unique request details
            string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            TimeSpan TimeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string oauth_timestamp = Convert.ToInt64(TimeSpan.TotalSeconds).ToString();

            // message api details        
            string resource_url = ConfigurationManager.AppSettings["Twitter_Endpoint"];
            string screen_name = ConfigurationManager.AppSettings["Twitter_Screen_Name"];

            // create oauth signature
            string baseFormat = "count={7}&oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}&trim_user=true";
            string baseString = string.Format(baseFormat, oauth_consumer_key, oauth_nonce, oauth_signature_method, oauth_timestamp, oauth_token, oauth_version, Uri.EscapeDataString(screen_name), limit.ToString());

            baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            string compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret), "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature = null;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            string headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", oauth_token=\"{4}\", oauth_signature=\"{5}\", oauth_version=\"{6}\"";
            string authHeader = string.Format(headerFormat, Uri.EscapeDataString(oauth_nonce), Uri.EscapeDataString(oauth_signature_method), Uri.EscapeDataString(oauth_timestamp), Uri.EscapeDataString(oauth_consumer_key), Uri.EscapeDataString(oauth_token), Uri.EscapeDataString(oauth_signature), Uri.EscapeDataString(oauth_version));

            // make the request
            ServicePointManager.Expect100Continue = false;

            string postBody = "screen_name=" + Uri.EscapeDataString(screen_name) + "&count=" + limit.ToString() + "&trim_user=true";
            resource_url += "?" + postBody;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse response = null;
            string responseData = string.Empty;

            try
            {
                response = await request.GetResponseAsync();
                responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
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
            }
            return responseData;
        }
    }
}

