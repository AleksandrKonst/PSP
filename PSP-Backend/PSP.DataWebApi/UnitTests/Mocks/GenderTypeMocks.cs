using Application.DTO.PassengerContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class GenderTypeMocks
{
    public static GenderType GetGenderType()
    {
        var result = new GenderType()
        {
            Code = "M",
            Gender = "мужской"
        };
        return result;
    }
    
    public static GenderTypeDTO GetGenderTypeDTO()
    {
        var result = new GenderTypeDTO()
        {
            Code = "M",
            Gender = "мужской"
        };
        return result;
    }
}