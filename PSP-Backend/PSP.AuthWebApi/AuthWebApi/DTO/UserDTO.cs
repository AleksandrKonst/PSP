namespace AuthWebApi.DTO;

public class UserDTO
{
    public string Id { get; set; }
    public string Login { get; set; }
    public string FIO { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string Role { get; set; }
}