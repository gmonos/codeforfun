using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using System.Xml;

namespace Zags.Web.Security
{
    public class JwtBearerAuthenticationUtility
    {
        private static string Authoriry = ConfigurationManager.AppSettings["ida:Authoriry"];
        private static string audience = ConfigurationManager.AppSettings["ida:Audience"];
        private static string secret = ConfigurationManager.AppSettings["ida:Secret"];
        private static string metaAddress = ConfigurationManager.AppSettings["ida:MetaDataAddress"];


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="secret"></param>
        /// <param name="metaAddress"></param>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="secret"></param>
        /// <param name="metaAddress"></param>
        public static void SetOwinJwtBearerAuthentication(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(BuildJwtBearerAuthenticationOptions(Authoriry, audience, secret, metaAddress));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private static JwtBearerAuthenticationOptions BuildJwtBearerAuthenticationOptions(string issuer, string audience, string secret, string metaAddress)
        {
            return new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,

                AllowedAudiences = new[] { audience },

                //IssuerSecurityTokenProviders = new IssuedSecurityTokenProvider[]
                //                                        {
                //                                            new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                //                                        },

                Provider = new OAuthBearerAuthenticationProvider
                {
                    OnValidateIdentity = context =>
                    {
                        context.OwinContext.Authentication.User = new ClaimsPrincipal(context.Ticket.Identity);
                        return Task.FromResult<object>(null);
                    },
                },

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningTokens = GetSigningCertificates(metaAddress)
                }
            };
        }
     
        
        
        /// <summary>
        ///  Returns a list of certificates from the specified metadata document.
        /// </summary>
        /// <param name="metadataAddress"></param>
        /// <returns></returns>
     
        private static List<X509SecurityToken> GetSigningCertificates(string metadataAddress)
        {
            List<X509SecurityToken> tokens = new List<X509SecurityToken>();

            if (metadataAddress == null)
            {
                throw new ArgumentNullException(metadataAddress);
            }

            using (XmlReader metadataReader = XmlReader.Create(metadataAddress))
            {
                MetadataSerializer serializer = new MetadataSerializer()
                {
                    // Do not disable for production code
                    CertificateValidationMode = X509CertificateValidationMode.None
                };

                EntityDescriptor metadata = serializer.ReadMetadata(metadataReader) as EntityDescriptor;

                if (metadata != null)
                {
                    SecurityTokenServiceDescriptor stsd = metadata.RoleDescriptors.OfType<SecurityTokenServiceDescriptor>().First();

                    if (stsd != null)
                    {
                        IEnumerable<X509RawDataKeyIdentifierClause> x509DataClauses = stsd.Keys.Where(key => key.KeyInfo != null && (key.Use == KeyType.Signing || key.Use == KeyType.Unspecified)).
                                                             Select(key => key.KeyInfo.OfType<X509RawDataKeyIdentifierClause>().First());

                        tokens.AddRange(x509DataClauses.Select(token => new X509SecurityToken(new X509Certificate2(token.GetX509RawData()))));
                    }
                    else
                    {
                        throw new InvalidOperationException("There is no RoleDescriptor of type SecurityTokenServiceType in the metadata");
                    }
                }
                else
                {
                    throw new Exception("Invalid Federation Metadata document");
                }
            }
            return tokens;
        }
    }
}