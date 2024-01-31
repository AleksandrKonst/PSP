namespace Application.DTO.ArmContextDTO.Delete;

public class DeleteCouponEventRequestDTO
{
    public string OperationType { get; set; }
    
    public int TicketType { get; set; }
    
    public string TicketNumber { get; set; }
    
    public string OperationDatetime { get; set; }
}