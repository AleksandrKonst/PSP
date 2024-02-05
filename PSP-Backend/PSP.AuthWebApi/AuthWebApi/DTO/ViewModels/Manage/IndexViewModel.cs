namespace AuthWebApi.DTO.ViewModels.Manage;

public class IndexViewModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public IEnumerable<UserDTO> Users { get; set; }
}