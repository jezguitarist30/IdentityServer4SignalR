using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer
{
    public class Config
    {
        /// <summary>
        /// Defines Identity Resources that can be requested by a user during authentication process.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This will be ported to a custom Identity Resource Store using SQL databse in the future.</remarks>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                 name: "custom.profile",
                 displayName: "Name",
                 claimTypes: new[] { "name", "email", "status" });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                customProfile
            };
        }

        /// <summary>
        /// Defines the API Resources associated to an authorized client.
        /// </summary>
        /// <returns></returns>
        ///<remarks>This will be ported to a custom API Resource Store using SQL databse in the future.</remarks>
        public static IEnumerable<ApiResource> GetApiResource()
        {
            List<string> claimTypes = new List<string>();

            return new List<ApiResource>
            {
                new ApiResource("SignalRDemo", "SignalR Demo Resources", new List<string>()
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                })                
            };
        }

        /// <summary>
        /// This registers clients that are allowed tp interact with the IDP.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This will be ported to a custom Client Store using SQL databse in the future.</remarks>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
               new Client
                {
                    ClientId = "MyClient",
                    ClientName = "FrontEnd Clients",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowedCorsOrigins = { "http://localhost:8080", "http://localhost:4200" },
                    ClientSecrets =
                    {
                        new Secret("my-client-secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "SignalRDemo"
                    }
                },
            };
        }
    }
}
