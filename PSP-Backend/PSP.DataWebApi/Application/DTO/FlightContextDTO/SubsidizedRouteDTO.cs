namespace Application.DTO.FlightContextDTO;

public class SubsidizedRouteDTO
{
    public int Id { get; set; }
    
    public string CityStartIataCode { get; set; }
    
    public string CityFinishIataCode { get; set; }
    
    public short Appendix { get; set; }
    
    public int FareAmount { get; set; }
    
    public int SubsidyAmount { get; set; }
    
    public string Currency { get; set; }
    
    public DateTime ValidityFrom { get; set; }
    
    public DateTime ValidityTo { get; set; }
    
    public List<string>? InteriorCities { get; set; }
}