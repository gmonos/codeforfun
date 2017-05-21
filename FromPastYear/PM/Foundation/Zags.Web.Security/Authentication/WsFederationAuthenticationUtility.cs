using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Metadata;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using System.Xml;

namespace Zags.Web.Security
{
    public class WsFederationAuthenticationUtility
    {
        private static string _Audience = "http://localhost:5000/"; //ConfigurationManager.AppSettings["ida:Audience"];
        private static string _MetaDataAddress = "https://zagsacsdev.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml"; //ConfigurationManager.AppSettings["ida:MetaDataAddress"];

        public static void SetWsFederationAuthentication(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWsFederationAuthentication(
                new WsFederationAuthenticationOptions
                {
                    Wtrealm = "http://localhost:5000/",
                    MetadataAddress = "https://zagsacsdev.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml",
                    Notifications = new WsFederationAuthenticationNotifications
                    {
                        SecurityTokenReceived = param =>
                        {
                            return Task.FromResult<object>(null);
                        },
                        AuthenticationFailed = param =>
                        {
                            return Task.FromResult<object>(null);
                        }
                    },
                    SecurityTokenHandlers = new SecurityTokenHandlerCollection
                    {
                        new System.IdentityModel.Tokens.SamlSecurityTokenHandler
                        {
                            CertificateValidator = X509CertificateValidator.None,
                           
                        }
                    }

                });

            AuthenticateAllRequests(app, "WS-Fed Auth (Primary)");

           
        }


        private static void AuthenticateAllRequests(IAppBuilder app, params string[] authenticationTypes)
        {
            app.Use((context, continuation) =>
            {
                if (context.Authentication.User != null &&
                    context.Authentication.User.Identity != null &&
                    context.Authentication.User.Identity.IsAuthenticated)
                {
                    return continuation();
                }
                else
                {
                    context.Authentication.Challenge(authenticationTypes);
                    return Task.Delay(0);
                }
            });
        }
    }
}