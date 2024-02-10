using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Auth;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType("Password")]
    public string Password { get; set; }
    [Required]
    public string ReturnUrl { get; set; }
}