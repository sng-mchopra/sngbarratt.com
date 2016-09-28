using Mandrill;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace jCtrl.WebApi.Services
{
    public class EmailService : IIdentityMessageService
    {


        public enum EmailType
        {
            User_Create_Account,
            User_Reset_Account,
            User_Update_Account,
            User_Verify_Email_Address,
            User_Marketing_Subscribe,
            User_Marketing_Unsubscribe,
            Cart_Reminder,
            Cart_Checkout,
            Quote_Request,
            Quote_Created,
            Quote_Approved,
            Quote_Declined,
            Order_Request,
            Order_Confirm,
            Order_Update,
            Order_Shipment,
            Order_Cancel,
            Order_Close,
            Order_Complete,
            /* 
            Pick_Created,
            Pick_InProgress,
            Pick_Confirmed,
            Pack_Created,
            Pack_InProgess,
            Pack_Confirmed,
            Payment_Failed,
            Invoice_Created,
            Credit_Raised, 
            */
            Product_Enquiry,
            General_Enquiry
        }


        public async Task SendAsync(IdentityMessage message)
        {
            await configMandrillAsync(message);
        }

        //public async Task SendAsync(IdentityMessage message, EmailType type, string headline, string branch, List<KeyValuePair<string,string>> metadata)
        //{
        //    await configMandrillAsync(message, type, headline, branch, metadata);
        //}


        //private async Task configMandrillAsync(IdentityMessage message)
        //{
        //    var myMessage = new Mandrill.Models.EmailMessage();

        //    myMessage.Subject = message.Subject;
        //    myMessage.Text = message.Body;
        //    myMessage.Html = message.Body;
        //    myMessage.AutoHtml = true;

        //    myMessage.To = new List<Mandrill.Models.EmailAddress> {
        //        new Mandrill.Models.EmailAddress(message.Destination)
        //    };                        

        //    await sendMandrillAsync(myMessage);
        //}

        private async Task configMandrillAsync(IdentityMessage message)
        {

            var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(message.Body);
            foreach (KeyValuePair<string, string> kv in settings)
            {
                System.Diagnostics.Debug.WriteLine(kv.Key + " : " + kv.Value);
            }

            string template = "";
            var myMessage = new Mandrill.Models.EmailMessage();
            var contents = new List<Mandrill.Models.TemplateContent>();

            // get type from subject
            var type = (EmailType)Enum.Parse(typeof(EmailType), message.Subject);

            switch (type)
            {
                case EmailType.User_Create_Account:

                    template = "Web_Text_Only";

                    myMessage.Tags = new List<string> { "usr_create", "sng_" + settings["branch"].ToString() };

                    myMessage.AddGlobalVariable("subject", settings["subject"]);

                    myMessage.AddMetadata("branch", settings["branch"]);
                    myMessage.AddMetadata("language", settings["language"]);
                    myMessage.AddMetadata("user", settings["user"]);

                    contents.Add(new Mandrill.Models.TemplateContent { Name = "branch", Content = settings["branch"] });
                    contents.Add(new Mandrill.Models.TemplateContent { Name = "headlinetext", Content = settings["title"] });
                    contents.Add(new Mandrill.Models.TemplateContent { Name = "bodytext", Content = settings["body"] });

                    myMessage.UrlStripQs = true;

                    break;
            }


            // add recipient
            myMessage.To = new List<Mandrill.Models.EmailAddress> { new Mandrill.Models.EmailAddress(message.Destination) };

            myMessage.FromEmail = "DoNotReply@sngbarratt.com";
            myMessage.FromName = "SNG Barratt";

            myMessage.Subject = settings["subject"];

            myMessage.AutoHtml = false;
            myMessage.AutoText = true;
            myMessage.Important = true;
            //myMessage.InlineCss = true;
            myMessage.TrackOpens = true;
            myMessage.TrackClicks = true;



            var sender = new MandrillApi(ConfigurationManager.AppSettings["Mandrill_Key"]);

            // Send the email.
            if (sender != null)
            {
                await sender.SendMessageTemplate(new Mandrill.Requests.Messages.SendMessageTemplateRequest(myMessage, template, contents));
            }
            else
            {
                //Trace.TraceError("Failed to create Mandrill API.");
                await Task.FromResult(0);
            }
        }






        //////// Use NuGet to install SendGrid (Basic C# client lib) 
        //////private async Task configSendGridasync(IdentityMessage message)
        //////{
        //////    var myMessage = new SendGridMessage();

        //////    myMessage.AddTo(message.Destination);
        //////    myMessage.From = new System.Net.Mail.MailAddress("taiseer@bitoftech.net", "Taiseer Joudeh");
        //////    myMessage.Subject = message.Subject;
        //////    myMessage.Text = message.Body;
        //////    myMessage.Html = message.Body;

        //////    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
        //////                                            ConfigurationManager.AppSettings["emailService:Password"]);

        //////    // Create a Web transport for sending email.
        //////    var transportWeb = new Web(credentials);

        //////    // Send the email.
        //////    if (transportWeb != null)
        //////    {
        //////        await transportWeb.DeliverAsync(myMessage);
        //////    }
        //////    else
        //////    {
        //////        //Trace.TraceError("Failed to create Web transport.");
        //////        await Task.FromResult(0);
        //////    }
        //////}
    }
}