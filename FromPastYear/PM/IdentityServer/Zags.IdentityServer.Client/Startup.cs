using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.IdentityModel.Claims;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Configuration;

[assembly: OwinStartup(typeof(Zags.IdentityServer.Client.Startup))]

namespace Zags.IdentityServer.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = ConfigurationManager.AppSettings["ids:Tenant"],
                ClientId = "mvc",
                ClientSecret = "mvc",
                RedirectUri = ConfigurationManager.AppSettings["ids:RedirectUri"],
                ResponseType = "code id_token token",
                Scope = "openid profile organizationapi",
                SignInAsAuthenticationType = "Cookies",
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = async n =>
                    {
                        var userInfo = await EndpointAndTokenHelper.CallUserInfoEndpoint(n.ProtocolMessage.AccessToken);

                        var newIdentity = new ClaimsIdentity(
                           n.AuthenticationTicket.Identity.AuthenticationType);

                        newIdentity.AddClaim(new System.Security.Claims.Claim("access_token", n.ProtocolMessage.AccessToken));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            newIdentity,
                            n.AuthenticationTicket.Properties);

                    }

                }
            });
        }
    }
}
