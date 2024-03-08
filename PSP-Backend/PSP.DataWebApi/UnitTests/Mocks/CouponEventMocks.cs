using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class CouponEventMocks
{
    public static CouponEvent GetCouponEvent()
    {
        var result = new CouponEvent()
        {
            Id = 1,
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
    
    public static CouponEventDTO GetCouponEventDTO()
    {
        var result = new CouponEventDTO()
        {
            Id = 1,
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
}