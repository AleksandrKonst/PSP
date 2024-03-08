using Application.DTO.FlightContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class QuotaCategoryMocks
{
    public static QuotaCategory GetQuotaCategory()
    {
        var result = new QuotaCategory()
        {
            Code = "invalid",
            Category = "Инвалидность",
            Appendices = new List<short>() {1},
            OneWayQuota = 4,
            RoundTripQuota = 2
        };
        return result;
    }
    
    public static QuotaCategoryDTO GetQuotaCategoryDTO()
    {
        var result = new QuotaCategoryDTO()
        {
            Code = "invalid",
            Category = "Инвалидность",
            Appendices = new List<short>() {1},
            OneWayQuota = 4,
            RoundTripQuota = 2
        };
        return result;
    }
}