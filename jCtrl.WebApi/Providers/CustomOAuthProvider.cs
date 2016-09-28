using jCtrl.Infrastructure;
using jCtrl.Services.Core.Domain;
using jCtrl.WebApi.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.Entity;

namespace jCtrl.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
     
            if (context.Request.Method == "OPTIONS")
            {
                // is preflight
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }

    
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrets once obtain access tokens. 
                //context.Validated();

                context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }


            using (var unitOfWork = new UnitOfWork(new jCtrlContext()))
            {
                client = unitOfWork.Clients.FindClient(context.ClientId);
            }


            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.IsSecure)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    string hashedClientSecret = jCtrl.Services.Core.Utils.Helpers.GetHash(clientSecret);

                    if (client.Secret != hashedClientSecret)
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.IsActive)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }


            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenTTL", client.RefreshTokenTTL.ToString());

            context.Validated();
            return Task.FromResult<object>(null);

        }
        
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

           
            //////    // get origin from request
            //////    var origin = context.Request.Headers.Get("Origin");
            //////if (origin != null)
            //////{
            //////    System.Diagnostics.Trace.Write("Request Origin: " + origin);

            //////    // set allowed origins based on info for client from db
            //////    var clientAllowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            //////    if (clientAllowedOrigin != null)
            //////    {
            //////        System.Diagnostics.Trace.Write("Allowed Origins: " + clientAllowedOrigin);

            //////        // check origin is on whitelist
            //////        var allowedOrigins = clientAllowedOrigin.Split(',');
            //////        if (allowedOrigins.Contains("*") || allowedOrigins.Contains(origin))
            //////        {
            //////            // set allowed origin header
            //////            if (context.OwinContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin")) context.OwinContext.Response.Headers.Remove("Access-Control-Allow-Origin");
            //////            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { origin });
            //////        }
            //////    }
            //////}


            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            UserAccount user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }


            //if (!user.EmailConfirmed)
            //{
            //    context.SetError("invalid_grant", "User email address is not confirmed.");
            //    return;
            //}


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("userId", user.Id));

            using (var unitOfWork = new UnitOfWork(new jCtrlContext()))
            {

                var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false,
                    c => c.Branch,
                    c => c.Language,
                    c => c.EmailAddresses);

                if (cust != null)
                {
                    identity.AddClaim(new Claim("custId", cust.Id.ToString()));
                    //identity.AddClaim(new Claim("branchId", cust.Branch_Id.ToString()));
                    //identity.AddClaim(new Claim("langId", cust.Language_Id.ToString()));

                    if (cust.Branch != null) { identity.AddClaim(new Claim("siteCode", cust.Branch.SiteCode)); }
                    if (cust.Language != null) { identity.AddClaim(new Claim("langCode", cust.Language.Code)); }

                }
            }

            identity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(identity));


            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                    { "as:user_id", (user.Id == null) ? string.Empty : user.Id },
                    { "userName", context.UserName }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");

                return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            newIdentity.AddClaim(new Claim(ClaimTypes.Name, context.Ticket.Identity.Name));
            newIdentity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(newIdentity));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

    }
}