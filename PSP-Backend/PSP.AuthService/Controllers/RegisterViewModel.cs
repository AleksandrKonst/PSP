using System.ComponentModel.DataAnnotations;

namespace PSP.AuthService.Controllers;

public class RegisterViewModel
{
    public string ReturnUrl { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType("Password")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; }
}