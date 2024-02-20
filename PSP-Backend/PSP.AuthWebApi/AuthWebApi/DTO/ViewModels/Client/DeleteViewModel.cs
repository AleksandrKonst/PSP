namespace AuthWebApi.DTO.ViewModels.Client;

public class DeleteViewModel
{
    public string ClientId { get; set; }
    public ICollection<string> AllowedGrantTypes { get; set; }
    public bool RequirePkce { get; set; }
    public bool RequireClientSecret { get; set; }
    public ICollection<string> RedirectUris { get; set; }
    public ICollection<string> AllowedScopes { get; set; }
    public bool AllowAccessTokensViaBrowser { get; set; }
    public bool AllowOfflineAccess { get; set; }
    public bool RequireConsent { get; set; }
    public ICollection<string> ClientSecrets { get; set; }
    public ICollection<string> PostLogoutRedirectUris { get; set; }
    public ICollection<string> AllowedCorsOrigins { get; set; }
}