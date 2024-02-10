namespace AuthWebApi.DTO.ViewModels.Manage;

public class IndexViewModel
{
    public string Search { get; set; }
    public int Page { get; set; }
    
    public int MaxPage { get; set; }
    public IEnumerable<UserDTO> Users { get; set; }
}