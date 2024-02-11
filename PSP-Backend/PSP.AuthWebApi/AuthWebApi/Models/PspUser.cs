using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.Models;

public class PspUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public string? Patronymic { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly? Birthday { get; set; }
}