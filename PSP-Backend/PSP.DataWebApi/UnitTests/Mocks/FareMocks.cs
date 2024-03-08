using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class FareMocks
{
    public static Fare GetFare()
    {
        var result = new Fare()
        {
            Code = "5NI",
            Amount = 760000,
            Currency = "RUB",
            Special = true
        };
        return result;
    }
    
    public static FareDTO GetFareDTO()
    {
        var result = new FareDTO()
        {
            Code = "5NI",
            Amount = 760000,
            Currency = "RUB",
            Special = true
        };
        return result;
    }
}