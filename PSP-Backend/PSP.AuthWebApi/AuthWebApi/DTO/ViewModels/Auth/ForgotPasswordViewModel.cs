using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Auth;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}