using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Auth;

public class ChangePasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Password { get; set; }
    
    [Required]
    public string NewPassword { get; set; }
    
    [Required]
    public string NewPasswordConfirm { get; set; }
}