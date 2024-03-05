using Application.DTO;
using Domain.Models;

namespace Test.Mocks;

public static class RouteMocks
{
    public static List<Flight> GetFlight()
    {
        var result = new List<Flight>()
        {
            new()
            {
                Code = 180,
                AirlineCode = "SU",
                DepartPlace = "VVO",
                DepartDatetimePlan = new DateTime(2023, 3, 1),
                ArrivePlace = "SVO",
                ArriveDatetimePlan = new DateTime(2023, 3, 1),
                PnrCode = "5NR",
                FareCode = "5NR",
                AirlineCodeNavigation = new Airline()
                {
                    IataCode = "SU",
                    NameRu = "ПАО «Аэрофлот»",
                    NameEn = "PJSC \"Aeroflot\"",
                    IcaoCode = "AFL",
                    RfCode = "СУ",
                    Country = "Россия",
                    ReportsUseFlightDataFact = true,
                    ReportsUseFirstTransferFlightDepartDate = true
                },
                ArrivePlaceNavigation = new Airport()
                {
                    IataCode = "SVO",
                    IcaoCode = "UUEE",
                    RfCode = "ШРМ",
                    Name = "Шереметьево",
                    CityIataCode = "MOW",
                    Latitude = (decimal) 55.972778,
                    Longitude = (decimal) 37.414722,
                    CityIataCodeNavigation = new City()
                    {
                        IataCode = "MOW",
                        Name = "Москва"
                    }
                },
                DepartPlaceNavigation = new Airport()
                {
                    IataCode = "VVO",
                    IcaoCode = "UHWW",
                    RfCode = "ВВО",
                    Name = "Кневичи",
                    CityIataCode = "VVO",
                    Latitude = (decimal) 55.972778,
                    Longitude = (decimal) 37.414722,
                    CityIataCodeNavigation = new City()
                    {
                        IataCode = "VVO",
                        Name = "Владивосток"
                    }
                },
                FareCodeNavigation = new Fare()
                {
                    Code = "5NR",
                    Amount = 76000,
                    Currency = "RUB",
                    Special = true
                }
            }
        };

        return result;
    }

    public static List<FlightViewModel> GetFlightViewModel()
    {
        var result = new List<FlightViewModel>()
        {
            new()
            {
                DepartPlace = "VVO",
                DepartDatetimePlan = new DateTime(2023, 3, 1),
                ArrivePlace = "SVO",
                ArriveDatetimePlan = new DateTime(2023, 3, 1),
                FareCode = "5NR",
                Fare =  new FareViewModel()
                {
                    Code = "5NR",
                    Amount = 76000,
                    Currency = "RUB",
                    Special = true
                },
                ArrivePlaceModel = new AirportViewModel()
                {
                    IataCode = "SVO",
                    IcaoCode = "UUEE",
                    RfCode = "ШРМ",
                    Name = "Шереметьево",
                    CityIataCode = "MOW",
                    Latitude = (decimal) 55.972778,
                    Longitude = (decimal) 37.414722
                },
                DepartPlaceModel = new AirportViewModel()
                {
                    IataCode = "VVO",
                    IcaoCode = "UHWW",
                    RfCode = "ВВО",
                    Name = "Кневичи",
                    CityIataCode = "VVO",
                    Latitude = (decimal) 55.972778,
                    Longitude = (decimal) 37.414722,
                },
                FlightSegments = new List<FlightSegmentViewModel>()
                {
                    new()
                    {
                        Code = 180,
                        AirlineCode = "SU",
                        DepartPlace = "VVO",
                        DepartDatetimePlan = new DateTime(2023, 3, 1),
                        ArrivePlace = "SVO",
                        ArriveDatetimePlan = new DateTime(2023, 3, 1),
                        Airline = new AirlineViewModel()
                        {
                            IataCode = "SU",
                            NameRu = "ПАО «Аэрофлот»",
                            NameEn = "PJSC \"Aeroflot\"",
                            IcaoCode = "AFL",
                            RfCode = "СУ",
                            Country = "Россия"
                        } 
                    }
                }
            }
        };
        
        return result;
    }
}