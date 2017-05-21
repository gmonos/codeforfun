using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Zags.IdentityServer.Host
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
                // Client de test
                new Client {
                    ClientId = "openIdConnectClient",
                    ClientName = "Example Implicit Client Application",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("superSecret".Sha256()),
                    },

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        //"role",
                        //"customAPI"
                    },

                    RedirectUris = new List<string> {"http://localhost:6224/signin-oidc"},
                    PostLogoutRedirectUris = new List<string> {"http://localhost:6224"}
                },

                // Application existante
                new Client {
                    ClientId = "ZagsApplicationClient",
                    ClientName = "Zags Application",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("ZagsSuperSecret".Sha256()),
                    },

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },

                    RedirectUris = new List<string> {"http://localhost:52086/signin-oidc"},
                    PostLogoutRedirectUris = new List<string> { "http://localhost:52086" }

                    //RedirectUris = new List<string> {"http://localhost:13531/signin-oidc"},
                    //PostLogoutRedirectUris = new List<string> { "http://localhost:13531" }
                }
            };
        }
    }

    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource {
                    Name = "customAPI",
                    DisplayName = "Custom API",
                    Description = "Custom API Access",
                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<Scope> {
                        new Scope("customAPI.read"),
                        new Scope("customAPI.write")
                    }
                }
            };
        }
    }

    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "scott",
                    Password = "password",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.GivenName, "scott"),
                        new Claim(JwtClaimTypes.FamilyName, "harris"),
                        new Claim(JwtClaimTypes.PreferredUserName, "emea_user@zagso365dev.onmicrosoft.com"),
                        new Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
