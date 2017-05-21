using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using Serilog;
using Serilog.Filters;
using System.Configuration;
using Zags.Logging;
using Zags.Logging.Middleware;
using Zags.Logging.Serilog;
using Zags.OrganizationService.API.Adapters.Configuration;
using Zags.OrganizationService.API.Adapters.Service;
using Zags.OrganizationService.Application;
using Zags.Web.Nancy.Unity.Bootstrapper;
using Zags.Web.Security;

[assembly: OwinStartup("StartupConfiguration", typeof(StartUp))]

namespace Zags.OrganizationService.API.Adapters.Service
{
    public class StartUp
    {    
        private static UnityContainer s_container;

        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .Enrich.WithProperty("ServiceName", "OrganizationService")
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.Console(outputTemplate: "{Timestamp: yyyy - MM - dd HH: mm: ss} ({ServiceName)} ({CorrelationToken})  [{Level}] {A} {Message}{NewLine}{Exception}")
                .WriteTo.Logger(c =>
                    c.Enrich.FromLogContext()
                    .WriteTo.MSSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, "Logs")
                    //.WriteTo.Console(outputTemplate: "{Timestamp: yyyy - MM - dd HH: mm: ss} ({ServiceName)} ({CorrelationToken})  [{Level}] {A} {Message}{NewLine}{Exception}")
                    .Filter.ByIncludingOnly(Matching.FromSource("DomainTrakingEvent")))
                .CreateLogger();

            LogManager.Use<SerilogFactory>();

            s_container = new UnityContainer();
            IoCConfiguration.Run(s_container);

            var configuration = OrganizationServerConfiguration.GetConfiguration();
            var uri = configuration.Address.Uri;
            Globals.HostName = uri.Host + ":" + uri.Port;

            //Middleware registration
            app.Use(typeof(CorrelationToken));
            app.Use(typeof(RequestLogging));
            app.Use(typeof(PerformanceLogging));


            JwtBearerAuthenticationUtility.SetOwinJwtBearerAuthentication(app);

            //WsFederationAuthenticationUtility.SetWsFederationAuthentication(app);
          
            //app.UseIdentityServerBearerTokenAuthentication(new
           //        IdentityServerBearerTokenAuthenticationOptions
           //         {
           //             Authority = "https://sts.windows.net/2840d00e-9024-4b3c-bc75-afd3f7386ef4/",
           //             RequiredScopes = new[] { "c7d29df4-6c51-4b42-8378-caf7185490f8" },                        
           //         });
           //var certificate = new X509Certificate2(Convert.FromBase64String("MIIC1DCCAbygAwIBAgIQGU8bZgi257BN+dMzrNaQSDANBgkqhkiG9w0BAQUFADATMREwDwYDVQQDEwhGaWxpcC1QQzAeFw0xNjAyMjEwNjQ4MzdaFw0xNzAyMjEwMDAwMDBaMBMxETAPBgNVBAMTCEZpbGlwLVBDMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2/Ze/ru74n5YRTQKQujQOx4P7poPDuVfSi7aiBa7BV4pbBGXtzU8Mwt7LhewJnvbtJVdOj3S/4ndD3Zl65zV4RtqPAtGI0MJnMLxPibKaqnvikhLj/K5EEJ4yqXXlRSbH1VwwHzFtHmxnZd2KlmpNKF4WHaOzInoYmi36sffoAaikP7vmvUcO88X4tMP/KWxp5JZo5cQmLcKO3XiRDq532gezItq/p/iucHukF3WRMOL/73wB9bUcBU2/GIkFyB7Ne0YmJfhUopyCZnRh0UQP3DKrO1iKCy1Lje+TMi8hOoCfok8u1ZaJuueXgSf/J2S+AEe3M8D4OoYo6W0p+ZebwIDAQABoyQwIjALBgNVHQ8EBAMCBDAwEwYDVR0lBAwwCgYIKwYBBQUHAwEwDQYJKoZIhvcNAQEFBQADggEBANT5ltvrZJMHZNVO8juAO+PxyCSYmvIKNO2vBIglewmoF4vfdyABnAoIzHgKn5uvq1oPJCeiUHoNpzBMQiWqGW+NNL6wfTsZyfM24+EMv0ZDvkdm/B356tTZbPi/Pg/4vqDqAxbS6eE+VpBlZPHfDqCzlYKL+Ahhaq+xS4G0FJCvjWFt/EncwnVijuur3VYV+KxteAE+2ClI3N60nBH4UiOyigZ3Mwk0ONYu2R8X/AVMNpjKYXyXEGSi/JrCCNvINmnP4+SWpfFjVD8DDFK9VVsM6tl0HPM8qy3VkipCCnLZ6MRRIhrDnj8FnOZxCq7aI5fP7WDiwHKC/2zsX6LcOGs="));
           
            //app.UseJwtBearerAuthentication(new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions
           //{
           //    AllowedAudiences = new[] { "http://localhost:8570/resources" },
           //    TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
           //    {
           //        ValidAudience = "http://localhost:8570/resources",
           //        ValidIssuer = "http://localhost:8570",
           //        IssuerSigningKey = new X509SecurityKey(certificate)
           //    }

            //});
            app.UseNancy(options => options.Bootstrapper = new NancyUnityBootstrapper(s_container));
    
        }

        
    }
}
