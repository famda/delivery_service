using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace DeliveryService.Services.IdentityServer {
   public class Config {

      //This class configures In memory users and resources -> this is only for testing purposes
      //This shouldn't be like this is production, everythhing should come from a DB

      public static IEnumerable<IdentityResource> GetIdentityResources() {
         return new List<IdentityResource>
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
         };
      }

      public static IEnumerable<ApiResource> GetApiResources() {
         return new List<ApiResource> {
            new ApiResource("routesApi", "Routes Api") {
               UserClaims ={"Role"}
            }
         };
      }

      public static IEnumerable<Client> GetClients() {
         return new List<Client>
         {
            new Client
            {
            ClientId = "swaggerui",
            ClientName = "Swagger UI",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RedirectUris = { "http://localhost:5001/swagger/o2c.html" },
            PostLogoutRedirectUris = { "http://localhost:5001/swagger/" },
            AllowRememberConsent = true,
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowedScopes = { "routesApi", ClaimTypes.Role }
            },
            new Client
            {
               ClientId = "client",
               AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
               ClientSecrets =  { new Secret("secret".Sha256()) },
               AllowedScopes = {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "routesApi",
                  ClaimTypes.Role
               }
            }
         };
      }

      public static List<TestUser> GetUsers() {
         return new List<TestUser>
         {
            new TestUser
            {
               SubjectId = "1",
               Username = "filipe",
               Password = "password",
               Claims = new List<Claim> { new Claim("Role", "Admin")}
            },
            new TestUser
            {
               SubjectId = "2",
               Username = "bob",
               Password = "password",
            }
         };
      }
   }

}
