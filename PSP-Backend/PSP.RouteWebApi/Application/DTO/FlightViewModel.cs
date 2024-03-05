namespace Application.DTO;

public class FlightViewModel
{
    public string DepartPlace { get; set; }
    
    public DateTime DepartDatetimePlan { get; set; }
    
    public string ArrivePlace { get; set; }
    
    public DateTime ArriveDatetimePlan { get; set; }
    
    public string FareCode { get; set; }
    
    public FareViewModel Fare { get; set; }
    
    public AirportViewModel ArrivePlaceModel { get; set; }
    
    public AirportViewModel DepartPlaceModel { get; set; }

    public ICollection<FlightSegmentViewModel> FlightSegments { get; set; }
}

public class FlightSegmentViewModel
{
    public int Code { get; set; }
    
    public string AirlineCode { get; set; }
    
    public string DepartPlace { get; set; }
    
    public string ArrivePlace { get; set; }
    
    public DateTime DepartDatetimePlan { get; set; }
    
    public DateTime ArriveDatetimePlan { get; set; }
    
    public AirlineViewModel Airline{ get; set; }
}

public class FareViewModel
{
    public string Code { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; }
    
    public bool Special { get; set; }
}

public class AirportViewModel
{
    public string IataCode { get; set; }
    
    public string IcaoCode { get; set; }
    
    public string? RfCode { get; set; }
    
    public string Name { get; set; }
    
    public string CityIataCode { get; set; }
    
    public decimal? Latitude { get; set; }
    
    public decimal? Longitude { get; set; }
}

public class AirlineViewModel
{
    public string IataCode { get; set; }
    
    public string NameRu { get; set; }
    
    public string NameEn { get; set; }
    
    public string IcaoCode { get; set; }
    
    public string? RfCode { get; set; }
    
    public string Country { get; set; }
}