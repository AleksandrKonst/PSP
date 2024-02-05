namespace AuthWebApi.DTO;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
}