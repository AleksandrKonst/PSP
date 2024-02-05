using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.Models;

public class PspUser : IdentityUser
{
    public string Surname { get; set; }
    
    public string? Patronymic { get; set; }
    
    public DateTime? Birthday { get; set; }
}