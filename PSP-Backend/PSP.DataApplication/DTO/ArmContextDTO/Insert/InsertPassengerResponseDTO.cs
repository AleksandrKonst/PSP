using PSP.DataApplication.DTO.ArmContextDTO.General;

namespace PSP.DataApplication.DTO.ArmContextDTO.Insert;

public class InsertPassengerResponseDTO
{
    public long Id { get; set; }
    
    public InsertTicketPropertiesDTO TicketProperties { get; set; }
    
    public List<InsertQuotaBalanceDTO> QuotaBalances { get; set; }
}

public class InsertTicketPropertiesDTO
{
    public bool PassengerTypesPreConfirmed { get; set; }
    
    public bool ContainsQuotaRoutes { get; set; }
}

public class InsertQuotaBalanceDTO
{
    public int Year { get; set; }
    
    public int UsedDocumentCount { get; set; }
    
    public bool Changed { get; set; }
    
    public List<CategoryBalanceDTO> CategoryBalances { get; set; }
}