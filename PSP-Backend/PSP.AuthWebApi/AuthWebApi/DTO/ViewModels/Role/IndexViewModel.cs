using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.DTO.ViewModels.Role;

public class IndexViewModel
{
    public string Search { get; set; }
    public int Page { get; set; }
    
    public int MaxPage { get; set; }
    public IEnumerable<RoleDTO> Roles { get; set; }
}