namespace AuthWebApi.DTO.ViewModels.Manage;

public class EditClaimViewModel
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public IEnumerable<ClaimDTO> Claims { get; set; }
}