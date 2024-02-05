namespace AuthWebApi.DTO.ViewModels.Manage;

public class CreateViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
    public string Role { get; set; }
}