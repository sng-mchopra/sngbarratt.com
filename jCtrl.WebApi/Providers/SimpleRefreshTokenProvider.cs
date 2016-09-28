using jCtrl.Infrastructure;
using jCtrl.Services;
using jCtrl.Services.Core.Domain;
using jCtrl.WebApi.Infrastructure;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jCtrl.WebApi.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];
            var userid = context.Ticket.Properties.Dictionary["as:user_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (var unitOfWork = new UnitOfWork(new jCtrlContext()))
            {
                var refreshTokenTTL = context.OwinContext.Get<string>("as:clientRefreshTokenTTL");
                string hashedTokenId = jCtrl.Services.Core.Utils.Helpers.GetHash(refreshTokenId);

                var token = new RefreshToken()
                {
                    Id = hashedTokenId,
                    Client_Id = Guid.Parse(clientid),
                    User_Id = userid,
                    IssuedTimestampUtc = DateTime.UtcNow,
                    ExpiresTimestampUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenTTL))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedTimestampUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresTimestampUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = await unitOfWork.RefreshTokens.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            System.Diagnostics.Debug.WriteLine("".PadLeft(20, '-'));
            System.Diagnostics.Debug.WriteLine("CONTEXT RESPONSE HEADERS");
            foreach (KeyValuePair<string, string[]> kvp in context.Response.Headers)
            {
                System.Diagnostics.Debug.WriteLine("Key: " + kvp.Key);
                System.Diagnostics.Debug.WriteLine("Values:");
                foreach (string value in kvp.Value)
                {
                    System.Diagnostics.Debug.WriteLine(value);
                }

            }

            System.Diagnostics.Debug.WriteLine("".PadLeft(20, '*'));
            System.Diagnostics.Debug.WriteLine("OWIN CONTEXT RESPONSE HEADERS");
            foreach (KeyValuePair<string, string[]> kvp in context.OwinContext.Response.Headers)
            {
                System.Diagnostics.Debug.WriteLine("Key: " + kvp.Key);
                System.Diagnostics.Debug.WriteLine("Values:");
                foreach (string value in kvp.Value)
                {
                    System.Diagnostics.Debug.WriteLine(value);
                }

            }

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = jCtrl.Services.Core.Utils.Helpers.GetHash(context.Token);

            using (var unitOfWork = new UnitOfWork(new jCtrlContext()))
            {
                var refreshToken = await unitOfWork.RefreshTokens.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = await unitOfWork.RefreshTokens.RemoveRefreshToken(hashedTokenId);
                }
            }
        }
    }
}