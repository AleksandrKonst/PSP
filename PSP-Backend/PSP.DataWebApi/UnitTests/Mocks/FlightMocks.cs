using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class FlightMocks
{
    public static Flight GetFlight()
    {
        var result = new Flight()
        {
            Code = 180,
            AirlineCode = "SU",
            DepartPlace = "VVO",
            DepartDatetimePlan = new DateTime(2023, 3, 1),
            ArrivePlace = "SVO",
            ArriveDatetimePlan = new DateTime(2023, 3, 1),
            PnrCode = "5NR",
            FareCode = "5NR"
        };
        return result;
    }
    
    public static FlightDTO GetFlightDTO()
    {
        var result = new FlightDTO()
        {
            Code = 180,
            AirlineCode = "SU",
            DepartPlace = "VVO",
            DepartDateTimePlan = new DateTime(2023, 3, 1),
            ArrivePlace = "SVO",
            ArriveDateTimePlan = new DateTime(2023, 3, 1),
            PnrCode = "5NR",
            FareCode = "5NR"
        };
        return result;
    }
}