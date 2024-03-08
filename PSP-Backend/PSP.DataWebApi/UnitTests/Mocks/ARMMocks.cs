using System.Dynamic;
using Application.DTO.ArmContextDTO.General;
using Application.DTO.ArmContextDTO.Insert;
using Application.DTO.ArmContextDTO.Search;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class ARMMocks
{
    public static Passenger GetPassenger()
    {
        var result = new Passenger()
        {
            Id = 18,
            Name = "Виталий",
            Surname = "Райко",
            Patronymic = "Дмитриевич",
            Birthdate = new DateOnly(2002,7,18),
            Gender = "M",
            PassengerTypes = new List<string>() {"invalid_23"},
            CouponEvents = new List<CouponEvent>() {
                new()
                {
                    Id = 30,
                    OperationType = "issued",
                    OperationDatetimeUtc = new DateTime(2023,3,1, 16, 10, 0).ToUniversalTime(),
                    OperationDatetimeTimezone = 3,
                    OperationPlace = "AVIA CENTER LLC (MOSCOW)",
                    PassengerId = 18,
                    DocumentTypeCode = "01",
                    DocumentNumber = "46464565656",
                    DocumentNumberLatin = "46464565656",
                    QuotaCode = "invalid",
                    FlightCode = 179,
                    TicketType = 1,
                    TicketNumber = "2344555790"
                }
            }
        };
        return result;
    }

    public static IEnumerable<QuotaCategory> GetQuotaCategory()
    {
        var result = new QuotaCategory()
        {
            Code = "invalid",
            Category = "Инвалидность",
            Appendices = new List<short>() {1},
            OneWayQuota = 4,
            RoundTripQuota = 2
        };
            
        return new List<QuotaCategory>() {result};
    }
    
    public static SearchPassengerResponseDTO GetSearchPassengerResponseDTO()
    {
        var result = new SearchPassengerResponseDTO()
        {
            PassengerData = new SelectPassengerDataDTO()
            {
                Birthdate = new DateOnly(2002,7,18),
                Gender = "M", 
                DocumentType = "01",
                DocumentNumber = "46464565656",
                DocumentNumbersLatin = new List<string>() {"46464565656"}
            },
            QuotaBalances = new List<SelectQuotaBalanceDTO>()
            {
                new()
                {
                    Year = 2023,
                    UsedDocumentsCount = 1,
                    CategoryBalances = new List<CategoryBalanceDTO>()
                    {
                        new()
                        {
                            Category = "invalid",
                            Available = 4,
                            Issued = 1,
                            Refund = 0,
                            Used = 0
                        }
                    }
                }
            },
            CouponEvents = new List<CouponEventDTO>()
            {
                new()
                {
                    Id = 30,
                    OperationType = "issued",
                    OperationDatetimeUtc = new DateTime(2023,3,1, 16, 10, 0).ToUniversalTime(),
                    OperationDatetimeTimezone = 3,
                    OperationPlace = "AVIA CENTER LLC (MOSCOW)",
                    PassengerId = 18,
                    DocumentTypeCode = "01",
                    DocumentNumber = "46464565656",
                    DocumentNumberLatin = "46464565656",
                    QuotaCode = "invalid",
                    FlightCode = 179,
                    TicketType = 1,
                    TicketNumber = "2344555790"
                }
            }
        };
        return result;
    }
    
    public static CouponEvent GetTicket()
    {
        var result = new CouponEvent()
        {
            Id = 30,
            OperationType = "issued",
            OperationDatetimeUtc = new DateTime(2023,3,1, 16, 10, 0).ToUniversalTime(),
            OperationDatetimeTimezone = 3,
            OperationPlace = "AVIA CENTER LLC (MOSCOW)",
            PassengerId = 18,
            DocumentTypeCode = "01",
            DocumentNumber = "46464565656",
            DocumentNumberLatin = "46464565656",
            QuotaCode = "invalid",
            FlightCode = 179,
            TicketType = 1,
            TicketNumber = "2344555790"
        };
        return result;
    }
    
    public static SearchTicketResponseDTO SearchTicketResponseDTO()
    {
        var result = new SearchTicketResponseDTO()
        {
            CouponEvents = new List<CouponEventDTO>()
            {
                new()
                {
                    Id = 30,
                    OperationType = "issued",
                    OperationDatetimeUtc = new DateTime(2023,3,1, 16, 10, 0).ToUniversalTime(),
                    OperationDatetimeTimezone = 3,
                    OperationPlace = "AVIA CENTER LLC (MOSCOW)",
                    PassengerId = 18,
                    DocumentTypeCode = "01",
                    DocumentNumber = "46464565656",
                    DocumentNumberLatin = "46464565656",
                    QuotaCode = "invalid",
                    FlightCode = 179,
                    TicketType = 1,
                    TicketNumber = "2344555790",
                }
            }
        };
        return result;
    }
    
    public static dynamic GetPassengerQuotaCountDTO()
    {
        dynamic result = new ExpandoObject();

        result.id = 1;
        result.passengerData = new SelectPassengerDataDTO()
        {
            Birthdate = new DateOnly(2002,7,18),
            Gender = "M",
            DocumentType = "01",
            DocumentNumber = "46464565656",
            DocumentNumbersLatin = new List<string>() {"46464565656"},
        };
        result.identityConfirmation = new
        {
            Confirmed = true,
            Code = "PIC-000000",
            Message = "Успешное подтверждение личности гражданина"
        };
        result.typeConfirmations = new List<SelectTypeConfirmationDTO>()
        {
            new()
            {
                Status = "confirmed",
                Code = "PTC-000000",
                Message = "Успешное подтверждение типа пассажира"
            }
        };
        result.QuotaBalances = new List<SelectQuotaBalanceDTO>()
        {
            new()
            {
                Year = 2023,
                UsedDocumentsCount = 1,
                CategoryBalances = new List<CategoryBalanceDTO>()
                {
                    new()
                    {
                        Category = "invalid",
                        Available = 4,
                        Issued = 1,
                        Refund = 0,
                        Used = 0
                    }
                }
            }
        };
        
        return result;
    }
    
    public static InsertPassengerRequestDTO GetInsertPassengerRequestDTO()
    {
        var result = new InsertPassengerRequestDTO()
        {
            OperationType = "issued",
            OperationDatetime = "2023-03-01 16:10:00.000",
            OperationPlace = "AVIA CENTER LLC (MOSCOW)",
            Passengers = new List<InsertPassengerDataDTO>()
            {
                new()
                {
                    Id = 1,
                    Name = "Виталий",
                    Surname = "Райко",
                    Patronymic = "Дмитриевич",
                    Birthdate = new DateOnly(2002,7,18),
                    Gender = "M",
                    DocumentType = "01",
                    DocumentNumber = "46464565656",
                    TicketType = 1,
                    TicketNumber = "2344555790",
                    QuotaBalancesYears = new List<int>() {2023}
                }
            },
            Coupons = new List<InsertCouponDTO>()
            {
                new()
                {
                    AirlineCode = "SU",
                    FlightNumber = 180,
                    OperationPlace = "AVIA CENTER LLC (MOSCOW)",
                    DepartPlace = "VVO",
                    DepartTimePlan = "2023-03-01 16:10:00.000",
                    ArrivePlace = "SVO",
                    ArriveTimePlan ="2023-03-01 16:10:00.000",
                    PnrCode = "5NR",
                    Fares = new List<InsertFaresDTO>()
                    {
                        new()
                        {
                            PassengerId = 1,
                            PassengerType = "invalid_23",
                            Code = "5NI",
                            Amount = 760000,
                            Currency = "RUB",
                            Special = true
                        }
                    }
                }
            }
        };
        return result;
    }
    
    public static InsertPassengerResponseDTO InsertPassengerResponseDTO()
    {
        var result = new InsertPassengerResponseDTO()
        {
            Id = 1,
            TicketProperties = new InsertTicketPropertiesDTO()
            {
                PassengerTypesPreConfirmed = true,
                ContainsQuotaRoutes = true
            },
            QuotaBalances = new List<InsertQuotaBalanceDTO>()
            {
                new()
                {
                    Year = 2023,
                    UsedDocumentCount = 1,
                    Changed = true,
                    CategoryBalances = new List<CategoryBalanceDTO>()
                    {
                        new()
                        {
                            Category = "invalid",
                            Available = 4,
                            Issued = 2,
                            Refund = 0,
                            Used = 0
                        }
                    }
                }
            }
        };
        return result;
    }
}