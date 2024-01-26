namespace PSP.DataApplication.DTO.FlightContextDTO;

public class FlightDTO
{
    public int Code { get; set; }
    
    public string AirlineCode { get; set; } = null!;
    
    public string DepartPlace { get; set; } = null!;
    
    public string ArrivePlace { get; set; } = null!;
    
    public List<string>? Fares { get; set; }
}