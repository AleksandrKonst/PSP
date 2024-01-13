namespace PSP.DataWebApi.Passenger_Context.DTO;

public class PassengerQuotaCountDTO
{
    public long PassengerId { get; set; }
    
    public string QuotaCategoriesCode { get; set; } = null!;
    
    public string QuotaYear { get; set; } = null!;
    
    public short IssuedCount { get; set; }
    
    public short RefundCount { get; set; }

    public short UsedCount { get; set; }

    public short AvailableCount { get; set; }
}