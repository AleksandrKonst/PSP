using Domain.Models;

namespace UnitTests.Mocks;

public static class AirlineMocks
{
    public static Airline GetAirline()
    {
        var result = new Airline()
        {
            IataCode = "SU",
            NameRu = "ПАО «Аэрофлот»",
            NameEn = "PJSC \"Aeroflot\"",
            IcaoCode = "AFL",
            RfCode = "СУ",
            Country = "Россия",
            ReportsUseFlightDataFact = true,
            ReportsUseFirstTransferFlightDepartDate = true
        };
        return result;
    }
}