using PSP.DataApplication.DTO.ArmContextDTO.Insert;

namespace PSP.DataApplication.DTO;

public class InsertPassengerRequestDTO
{
    public string OperationType { get; set; }
    
    public string OperationDatetime { get; set; }
    
    public string OperationPlace { get; set; }
    
    public List<InsertPassengerDataDTO> Passengers { get; set; }
    
    public List<InsertCouponDTO> Coupons { get; set; }
}