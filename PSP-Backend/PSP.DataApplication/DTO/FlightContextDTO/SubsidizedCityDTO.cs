namespace PSP.DataApplication.DTO.FlightContextDTO;

public class SubsidizedCityDTO
{
    public string CityStartIataCode { get; set; } = null!;
    
    public string CityFinishIataCode { get; set; } = null!;
    
    public short Appendix { get; set; }
    
    public int SubsidyAmount { get; set; }
    
    public string Currency { get; set; }
}