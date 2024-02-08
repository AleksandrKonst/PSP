namespace AuthWebApi.DTO.ViewModels.Manage;

public class DeleteViewModel
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public DateTime? Birthday { get; set; }
}