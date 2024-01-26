namespace PSP.DataApplication.DTO.FlightContextDTO;

public class CouponEventDTO
{
    public long Id { get; set; }
    
    public string OperationType { get; set; }
    
    public DateTime OperationDatetimeUtc { get; set; }
    
    public short OperationDatetimeTimezone { get; set; }
    
    public string? OperationPlace { get; set; }
    
    public long PassengerId { get; set; }
    
    public short TicketType { get; set; }
    
    public int FlightCode { get; set; }
    
    public string FareCode { get; set; }
}