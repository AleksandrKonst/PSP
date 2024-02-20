namespace AuthWebApi.DTO.ViewModels.Manage;

public class DeleteViewModel
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateOnly? Birthday { get; set; }
}