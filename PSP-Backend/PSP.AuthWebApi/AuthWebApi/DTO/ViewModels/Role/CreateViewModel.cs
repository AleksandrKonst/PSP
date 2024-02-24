using System.ComponentModel.DataAnnotations;

namespace AuthWebApi.DTO.ViewModels.Role;

public class CreateViewModel
{
    [Required]
    public string Name { get; set; }
}