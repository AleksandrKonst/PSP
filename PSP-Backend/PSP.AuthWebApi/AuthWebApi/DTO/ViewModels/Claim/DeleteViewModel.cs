namespace AuthWebApi.DTO.ViewModels.Claim;

public class DeleteViewModel
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}