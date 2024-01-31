namespace Application.DTO;

public class FlightViewModel
{
    public int Code { get; set; }
    
    public string AirlineCode { get; set; }
    
    public string DepartPlace { get; set; }
    
    public DateTime DepartDatetimePlan { get; set; }
    
    public string ArrivePlace { get; set; }
    
    public DateTime ArriveDatetimePlan { get; set; }
    
    public string FareCode { get; set; }

    public AirlineViewModel Airline{ get; set; }
    
    public AirportViewModel ArrivePlaceModel { get; set; }
    
    public AirportViewModel DepartPlaceModel { get; set; }

    public ICollection<FlightSegmentViewModel> FlightSegments { get; set; }

    public FareViewModel Fare { get; set; }
}

public class FlightSegmentViewModel
{
    public int FlightCode { get; set; }
    
    public short FlightPart { get; set; }
    
    public string DepartPlace { get; set; } = null!;
    
    public string ArrivePlace { get; set; } = null!;
    
    public DateTime FlightDepartDatetime { get; set; }
    
    public DateTime FlightArriveDatetime { get; set; }
}

public class FareViewModel
{
    public string Code { get; set; } = null!;
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; } = null!;
    
    public bool Special { get; set; }
}

public class AirportViewModel
{
    public string IataCode { get; set; } = null!;
    
    public string IcaoCode { get; set; } = null!;
    
    public string? RfCode { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string CityIataCode { get; set; } = null!;
}

public class AirlineViewModel
{
    public string IataCode { get; set; } = null!;
    
    public string NameRu { get; set; } = null!;
    
    public string NameEn { get; set; } = null!;
    
    public string IcaoCode { get; set; } = null!;
    
    public string? RfCode { get; set; }
    
    public string Country { get; set; } = null!;
}