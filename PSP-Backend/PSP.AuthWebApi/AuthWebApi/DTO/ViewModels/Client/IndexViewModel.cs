using AuthWebApi.Models;

namespace AuthWebApi.DTO.ViewModels.Client;

public class IndexViewModel
{
    public string Search { get; set; }
    public int Page { get; set; }
    
    public int MaxPage { get; set; }
    public IEnumerable<string> Clients { get; set; }
}