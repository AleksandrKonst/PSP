using Application.DTO.ArmContextDTO.Select;
using Application.DTO.FlightContextDTO;

namespace Application.DTO.PassengerContextDTO;

public class PassengerQuotaDTO
{
    public List<SelectQuotaBalanceDTO> QuotaBalances { get; set; }
    
    public List<PassangerCouponEventDTO> CouponEvents { get; set; }
}

public class PassangerCouponEventDTO
{
    public long Id { get; set; }
    
    public string OperationType { get; set; }
    
    public DateTime OperationDatetimeUtc { get; set; }
    
    public short OperationDatetimeTimezone { get; set; }
    
    public string? OperationPlace { get; set; }
    
    public long PassengerId { get; set; }
    
    public string QuotaCode { get; set; }

    public int FlightCode { get; set; }

    public short TicketType { get; set; }
    
    public string TicketNumber { get; set; }
}