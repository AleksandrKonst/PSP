namespace AuthWebApi.DTO;

public class ClaimDTO
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}