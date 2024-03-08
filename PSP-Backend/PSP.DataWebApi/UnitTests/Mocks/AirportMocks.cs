using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class AirportMocks
{
    public static Airport GetAirport()
    {
        var result = new Airport()
        {
            IataCode = "SVO",
            IcaoCode = "UUEE",
            RfCode = "ШРМ",
            Name = "Шереметьево",
            CityIataCode = "MOW",
            Latitude = (decimal) 55.972778,
            Longitude = (decimal) 37.414722
        };
        return result;
    }
    
    public static AirportDTO GetAirportDTO()
    {
        var result = new AirportDTO()
        {
            IataCode = "SVO",
            IcaoCode = "UUEE",
            RfCode = "ШРМ",
            Name = "Шереметьево",
            CityIataCode = "MOW",
            Latitude = (decimal) 55.972778,
            Longitude = (decimal) 37.414722
        };
        return result;
    }
}