using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Client;

public class CreateViewModel
{
    [Required]
    public string ClientId { get; set; }
    [Required]
    public string AllowedGrantTypes { get; set; }
    [Required]
    public bool RequirePkce { get; set; }
    [Required]
    public bool RequireClientSecret { get; set; }
    public string? RedirectUris { get; set; }
    public string? AllowedScopes { get; set; }
    [Required]
    public bool AllowAccessTokensViaBrowser { get; set; }
    [Required]
    public bool AllowOfflineAccess { get; set; }
    [Required]
    public bool RequireConsent { get; set; }
    public string? ClientSecrets { get; set; }
    public string? PostLogoutRedirectUris { get; set; }
    public string? AllowedCorsOrigins { get; set; }
}