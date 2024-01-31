using IdentityServer4;
using IdentityServer4.Models;

namespace PSP.AuthService;

public static class Config
{
    public static IEnumerable<Client> Clients =>
        new List<Client> 
        {
            new()
            {
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientId = "passengerClient",
                ClientSecrets = { new Secret("passengerClientSecret".Sha256()) },
                AllowedScopes = { "api" },
            },
            new()
            {
                ClientId = "postman",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                AllowedScopes = 
                {
                    "api",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile 
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
                RequireConsent = false
            },
            new()
            {
                ClientId = "angular",

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200" },

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    "api",
                },

                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new(){Name = "api"},
        };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            // new ApiResource("api")
        };
}