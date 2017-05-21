using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using IdentityServer3.WsFederation.Configuration;
using IdentityServer3.WsFederation.Models;
using System.Collections.Generic;
using IdentityServer3.WsFederation.Services;
using Microsoft.Owin.Security.OpenIdConnect;
using Serilog;

[assembly: OwinStartup(typeof(Zags.IdentityServer.Startup))]

namespace Zags.IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .CreateLogger();

            app.Map("/identity", idsrvApp =>
            {
                var inMemoryManager = new InMemoryManager();
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(inMemoryManager.GetUsers())
                    .UseInMemoryClients(inMemoryManager.GetClients())
                    .UseInMemoryScopes(inMemoryManager.GetScopes());

                var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
                var options = new IdentityServerOptions
                {
                    SigningCertificate = new X509Certificate2(certificate, ConfigurationManager.AppSettings["SigningCertificatePassword"]),
                    RequireSsl = false, //Not in production
                    Factory = factory,
                    PluginConfiguration = ConfigurePlugins,
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders
                    },

                };

                idsrvApp.UseIdentityServer(options);
            });
        }
        private static void ConfigurePlugins(IAppBuilder pluginApp, IdentityServerOptions options)
        {
            var wsFedOptions = new WsFederationPluginOptions(options);

            // data sources for in-memory services
            wsFedOptions.Factory.Register(new Registration<IEnumerable<RelyingParty>> (RelyingParties.Get()));
            wsFedOptions.Factory.RelyingPartyService = new Registration<IRelyingPartyService> (typeof(InMemoryRelyingPartyService));

            pluginApp.UseWsFederationPlugin(wsFedOptions);
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var aad = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "AzureAd",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,
                Authority = "https://login.microsoftonline.com/katsoupitsouhotmail.onmicrosoft.com",
                ClientId = "b0d36085-a8ab-4f18-b10b-c0384eefbab2",
                ClientSecret = "Qw8vTzvMP8gFIiwhRRJKnaeNRKDhfMZnU0LwBx3NkmI=",
                RedirectUri = "http://localhost:8570/identity/signin-azuread"
            };

            app.UseOpenIdConnectAuthentication(aad);
        }
    }
}
