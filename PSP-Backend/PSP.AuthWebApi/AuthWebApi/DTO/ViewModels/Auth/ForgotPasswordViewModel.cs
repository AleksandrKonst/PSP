using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Auth;

public class ForgotPasswordViewModel
{
    public string? ReturnUrl { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}