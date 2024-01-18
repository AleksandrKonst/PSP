namespace PSP.DataWebApi.ARM_Context.DTO;

public class QuotBalanceDTO
{
    public string Category { get; set; }

    public int Available { get; set; }
    
    public int Issued { get; set; }
    
    public int Refund { get; set; }
    
    public int Used { get; set; }
}