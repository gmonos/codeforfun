using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Zags.IdentityServer
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "cma@gmail.com",
                    Username ="cma",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.Name, "Chris Mal")
                    }
                }
            };
        }

        public IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read User Data"
                },
                new Scope
                    {
                        Name = "organizationapi",
                        DisplayName = "Organization API Scope",
                        Type = ScopeType.Resource,
                        Enabled = true
                    },
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Flow = Flows.Hybrid,
                    RequireConsent = true,
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("mvc".Sha256())
                        },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:14229/",
                        "https://www.getpostman.com/oauth2/callback"
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read",
                        "organizationapi"
                    },
                }

            };
        }

    }
}