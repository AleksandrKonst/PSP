namespace AuthWebApi.DTO.ViewModels.Claim;

public class IndexViewModel
{
    public string Search { get; set; }
    public int Page { get; set; }
    public int MaxPage { get; set; }
    public IEnumerable<ClaimDTO> Claims { get; set; }
    
}