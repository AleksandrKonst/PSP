using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO;

public class ChangePasswordDTO
{
    [Required]
    [DataType("Password")]
    public string CurrentPassword { get; set; }
    [Required]
    [DataType("Password")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; }
}