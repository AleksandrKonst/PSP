namespace Application.DTO.FlightContextDTO;

public class FareDTO
{
    public string Code { get; set; }
    
    public decimal Amount { get; set; }

    public string Currency { get; set; }
    
    public bool Special { get; set; }
}