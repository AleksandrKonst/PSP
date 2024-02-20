namespace Application.DTO.FlightContextDTO;

public class QuotaCategoryDTO
{
    public string Code { get; set; }
    
    public string Category { get; set; }
    
    public List<short> Appendices { get; set; }
    
    public short OneWayQuota { get; set; }
    
    public short RoundTripQuota { get; set; }
}