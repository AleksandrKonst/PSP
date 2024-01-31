using Application.DTO.ArmContextDTO.Select;
using Application.DTO.FlightContextDTO;

namespace Application.DTO.ArmContextDTO.Search;

public class SearchPassengerResponseDTO
{
    public SelectPassengerDataDTO PassengerData { get; set; }
    
    public List<SelectQuotaBalanceDTO> QuotaBalances { get; set; }
    
    public List<CouponEventDTO> CouponEvents { get; set; }
}