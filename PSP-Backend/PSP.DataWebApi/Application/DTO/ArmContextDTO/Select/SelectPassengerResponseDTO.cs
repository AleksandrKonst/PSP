using Application.DTO.ArmContextDTO.General;

namespace Application.DTO.ArmContextDTO.Select;

public class SelectPassengerDataDTO
{
    public DateOnly Birthdate { get; set; }

    public string Gender { get; set; }
    
    public string DocumentType { get; set; }
    
    public string DocumentNumber { get; set; }
    
    public List<string>? DocumentNumbersLatin { get; set; }
}

public class SelectTypeConfirmationDTO
{
    public string Status { get; set; }
    
    public string Code { get; set; }
    
    public string Message { get; set; }
}

public class SelectQuotaBalanceDTO
{
    public int Year { get; set; }
    
    public int UsedDocumentsCount { get; set; }
    
    public List<CategoryBalanceDTO> CategoryBalances { get; set; }
}