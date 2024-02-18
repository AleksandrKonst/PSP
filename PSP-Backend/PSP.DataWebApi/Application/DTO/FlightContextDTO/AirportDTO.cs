namespace Application.DTO.FlightContextDTO;

public class AirportDTO
{
    public string IataCode { get; set; }
    
    public string IcaoCode { get; set; }
    
    public string? RfCode { get; set; }
    
    public string Name { get; set; }
    
    public string CityIataCode { get; set; }
    
    public decimal? Latitude { get; set; }
    
    public decimal? Longitude { get; set; }
}