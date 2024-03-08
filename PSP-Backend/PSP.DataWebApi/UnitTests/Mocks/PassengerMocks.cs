using Application.DTO.ArmContextDTO.General;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.PassengerContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class PassengerMocks
{
    public static Passenger GetPassenger()
    {
        var result = new Passenger()
        {
            Id = 1,
            Name = "Виталий",
            Surname = "Райко",
            Patronymic = "Дмитриевич",
            Birthdate = new DateOnly(2002,7,18),
            Gender = "M",
            PassengerTypes = new List<string>() {"invalid_23"}
        };
        return result;
    }
    
    public static PassengerDTO GetPassengerDTO()
    {
        var result = new PassengerDTO()
        {
            Id = 1,
            Name = "Виталий",
            Surname = "Райко",
            Patronymic = "Дмитриевич",
            Birthdate = new DateOnly(2002,7,18),
            Gender = "M",
            PassengerTypes = new List<string>() {"invalid_23"}
        };
        return result;
    }
    
    public static PassengerQuotaDTO GetPassengerQuotaDTO()
    {
        var result = new PassengerQuotaDTO()
        {
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
            CouponEvents = new List<PassangerCouponEventDTO>() {
                new()
                {
                    Id = 30,
                    OperationType = "issued",
                    OperationDatetimeUtc = new DateTime(2023,3,1, 16, 10, 0).ToUniversalTime(),
                    OperationDatetimeTimezone = 3,
                    OperationPlace = "AVIA CENTER LLC (MOSCOW)",
                    PassengerId = 1,
                    QuotaCode = "invalid",
                    FlightCode = 179,
                    TicketType = 1,
                    TicketNumber = "2344555790"
                }
            }
        };
        return result;
    }
    
    public static Passenger GetPassengerQuota()
    {
        var result = new Passenger()
        {
            Id = 1,
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
                    PassengerId = 1,
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
}