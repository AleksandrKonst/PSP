namespace PSP.DataWebApi.Contexts.Passenger_Context.DTO;

public class PostPassengerTypeDTO
{
    public string Code { get; set; } = null!;
    
    public string Type { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public List<short> Appendices { get; set; } = null!;
    
    public List<string>? QuotaCategories { get; set; }
}