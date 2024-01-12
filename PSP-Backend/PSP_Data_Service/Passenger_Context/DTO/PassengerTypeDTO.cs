namespace PSP_Data_Service.Passenger_Context.DTO;

public class PassengerTypeDTO
{
    public string Code { get; set; } = null!;
    
    public string Type { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public List<short> Appendices { get; set; } = null!;
    
    public List<string>? QuotaCategories { get; set; }
}