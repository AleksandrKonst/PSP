using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Manage;

public class ChangePasswordViewModel
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string NormalizedUserName { get; set; }
    [Required]
    [DataType("Password")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; }
}