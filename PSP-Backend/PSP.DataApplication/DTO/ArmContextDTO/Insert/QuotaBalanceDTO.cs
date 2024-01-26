using PSP.DataApplication.DTO.ArmContextDTO.Select;

namespace PSP.DataApplication.DTO.ArmContextDTO.Insert;

public class QuotaBalanceDTO
{
    public int Year { get; set; }
    
    public int UsedDocumentCount { get; set; }
    
    public bool Changed { get; set; }
    
    public List<CategoryBalanceDTO> CategoryBalances { get; set; }
}