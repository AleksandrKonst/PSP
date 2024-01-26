namespace PSP.DataApplication.DTO.ArmContextDTO.Select;

public class CategoryBalanceDTO
{
    public string Category { get; set; }

    public int Available { get; set; }
    
    public int Issued { get; set; }
    
    public int Refund { get; set; }
    
    public int Used { get; set; }
}