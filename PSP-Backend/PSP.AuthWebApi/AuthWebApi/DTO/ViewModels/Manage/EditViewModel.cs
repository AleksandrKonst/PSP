using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Manage;

public class EditViewModel
{
    public string Username { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }
    [Required]
    [DataType("Password")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; }
}