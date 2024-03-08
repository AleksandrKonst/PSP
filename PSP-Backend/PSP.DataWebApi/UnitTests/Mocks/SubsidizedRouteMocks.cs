using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class SubsidizedRouteMocks
{
    public static SubsidizedRoute GetSubsidizedRoute()
    {
        var result = new SubsidizedRoute()
        {
            Id = 1,
            CityStartIataCode = "DYR",
            CityFinishIataCode = "ZIA",
            Appendix = 1,
            FareAmount = 1350000,
            SubsidyAmount = 1350000,
            Currency = "RUB",
            ValidityFrom = new DateTime(2021,12,25),
            ValidityTo = new DateTime(3333,12,31),
            InteriorCities = new List<string>()
        };
        return result;
    }
    
    public static SubsidizedRouteDTO GetSubsidizedRouteDTO()
    {
        var result = new SubsidizedRouteDTO()
        {
            Id = 1,
            CityStartIataCode = "DYR",
            CityFinishIataCode = "ZIA",
            Appendix = 1,
            FareAmount = 1350000,
            SubsidyAmount = 1350000,
            Currency = "RUB",
            ValidityFrom = new DateTime(2021,12,25),
            ValidityTo = new DateTime(3333,12,31),
            InteriorCities = new List<string>()
        };
        return result;
    }
}