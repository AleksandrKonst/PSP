namespace PSP.DataApplication.DTO.PassengerContextDTO;

public class PassengerQuotaCountDTO
{
    public long PassengerId { get; set; }
    
    public string QuotaCategoriesCode { get; set; }
    
    public string QuotaYear { get; set; }
    
    public short IssuedCount { get; set; }
    
    public short RefundCount { get; set; }

    public short UsedCount { get; set; }

    public short AvailableCount { get; set; }
}