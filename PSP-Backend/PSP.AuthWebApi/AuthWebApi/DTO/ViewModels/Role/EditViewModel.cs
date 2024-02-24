using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Role;

public class EditViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string NormalizedName { get; set; }
}