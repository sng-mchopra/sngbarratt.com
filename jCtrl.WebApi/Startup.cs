using System;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using jCtrl.WebApi.Providers;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security;
using System.Web.Http;
using jCtrl.WebApi.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using jCtrl.WebApi.App_Start;
using jCtrl.Infrastructure;
using Microsoft.Practices.Unity;
using jCtrl.Services;
using Unity.WebApi;
using System.Web.Cors;
using System.Linq;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(jCtrl.WebApi.Startup))]

namespace jCtrl.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // setup cors policy
            var corsPolicy = new CorsPolicy
            {
                AllowAnyOrigin = true,
                // AllowAnyHeader = true,
                // AllowAnyMethod = true,   
                SupportsCredentials = true
            };

            // add headers
            foreach (var hdr in new string[] { "Authorization", "Content-Type", "Origin", "X-Requested-With", "Accept", "Cache-Control" }) { corsPolicy.Headers.Add(hdr); }

            // add methods
            foreach (var mth in new string[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" }) { corsPolicy.Methods.Add(mth); }

            // add origins
            var allowedOrigins = ConfigurationManager.AppSettings["AllowedOrigins"];
            if (allowedOrigins != null)
            {
                var origins = allowedOrigins.Split(',').Select(x => x.Trim());
                if (origins.Any())
                {
                    if (!origins.Contains("*"))
                    {
                        corsPolicy.AllowAnyOrigin = false;

                        // add each whitelisted origin
                        foreach (var origin in origins) { corsPolicy.Origins.Add(origin); }
                    }
                }
            }

            // setup cors options
            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider { PolicyResolver = context => Task.FromResult(corsPolicy) }
            };

            // enable cors
            app.UseCors(corsOptions);

            // setup JWT tokens auth
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);


            var httpConfig = new HttpConfiguration();

            ConfigureWebApi(httpConfig);

            app.UseWebApi(httpConfig);

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(jCtrlContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/auth/token"),
                Provider = new CustomOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30), // should this come from the TTL for the client ?
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["JWT_Issuer"]),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = ConfigurationManager.AppSettings["JWT_Issuer"];
            var audienceId = ConfigurationManager.AppSettings["JWT_AudienceID"];
            var audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["JWT_AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            config.DependencyResolver = new UnityDependencyResolver(container);

            AutoMapperConfig.RegisterMappings();

            config.MapHttpAttributeRoutes();

             //force https for entire api
            config.Filters.Add(new jCtrl.WebApi.Filters.ForceHttpsAttribute());

            // use gzip compression for entire api
            config.Filters.Add(new jCtrl.WebApi.Filters.GZipCompressionAttribute());

            // validate querystring parameters for entire api
            config.Filters.Add(new jCtrl.WebApi.Filters.ValidateQueryStringParametersAttribute());

            // validate model state for entire api
            config.Filters.Add(new jCtrl.WebApi.Filters.ValidateModelStateAttribute());

            // return resposnes in json format
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // remove xml formatter
            var appXmlType = config.Formatters.OfType<XmlMediaTypeFormatter>().FirstOrDefault();
            if (appXmlType != null) { config.Formatters.Remove(appXmlType); }


        }
    }
}
