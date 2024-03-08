using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class CityMocks
{
    public static City GetCity()
    {
        var result = new City()
        {
            IataCode = "MOW",
            Name = "Москва"
        };
        return result;
    }
    
    public static CityDTO GetCityDTO()
    {
        var result = new CityDTO()
        {
            IataCode = "MOW",
            Name = "Москва"
        };
        return result;
    }
}