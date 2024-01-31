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
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(){Name = "api"},
        };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>{};
    
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>{};
}