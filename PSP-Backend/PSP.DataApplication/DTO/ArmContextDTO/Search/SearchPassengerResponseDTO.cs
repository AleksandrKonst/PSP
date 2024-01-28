using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.DTO.FlightContextDTO;

namespace PSP.DataApplication.DTO.ArmContextDTO.Search;

public class SearchPassengerResponseDTO
{
    public SelectPassengerDataDTO PassengerData { get; set; }
    
    public List<SelectQuotaBalanceDTO> QuotaBalances { get; set; }
    
    public List<CouponEventDTO> CouponEvents { get; set; }
}