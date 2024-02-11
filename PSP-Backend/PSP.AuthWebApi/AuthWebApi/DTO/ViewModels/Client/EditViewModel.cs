namespace AuthWebApi.DTO.ViewModels.Client;

public class EditViewModel
{
    public string ClientId { get; set; }
    public string AllowedGrantTypes { get; set; }
    public bool RequirePkce { get; set; }
    public bool RequireClientSecret { get; set; }
    public string? RedirectUris { get; set; }
    public string? AllowedScopes { get; set; }
    public bool AllowAccessTokensViaBrowser { get; set; }
    public bool AllowOfflineAccess { get; set; }
    public bool RequireConsent { get; set; }
    public string? ClientSecrets { get; set; }
    public string? PostLogoutRedirectUris { get; set; }
    public string? AllowedCorsOrigins { get; set; }
}