using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Manage;

public class EditViewModel
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string NormalizedUserName { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public bool EmailConfirmed { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly? Birthday { get; set; }
    [Required]
    public string Role { get; set; }
}