namespace Application.DTO.FlightContextDTO;

public class CouponEventDTO
{
    public long Id { get; set; }
    
    public string OperationType { get; set; }
    
    public DateTime OperationDatetimeUtc { get; set; }
    
    public short OperationDatetimeTimezone { get; set; }
    
    public string? OperationPlace { get; set; }
    
    public long PassengerId { get; set; }
    
    public string DocumentTypeCode { get; set; }
    
    public string DocumentNumber { get; set; }
    
    public string DocumentNumberLatin { get; set; }
    
    public string QuotaCode { get; set; }

    public int FlightCode { get; set; }

    public short TicketType { get; set; }
    
    public string TicketNumber { get; set; }
}