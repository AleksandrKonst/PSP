namespace Application.DTO.FlightContextDTO;

public class AirlineDTO
{
    public string IataCode { get; set; }
    
    public string NameRu { get; set; }
    
    public string NameEn { get; set; }
    
    public string IcaoCode { get; set; }
    
    public string? RfCode { get; set; }
    
    public string Country { get; set; }
    
    public bool ReportsUseFlightDataFact { get; set; }
    
    public bool ReportsUseFirstTransferFlightDepartDate { get; set; }
}