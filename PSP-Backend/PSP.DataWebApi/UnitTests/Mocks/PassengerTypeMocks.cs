using Application.DTO.PassengerContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class PassengerTypeMocks
{
    public static PassengerType GetPassengerType()
    {
        var result = new PassengerType()
        {
            Code = "child",
            Type = "ребенок",
            Description = "Сопровождаемый ребенок до 12 лет с предоставлением места",
            Appendices = new List<short>() {1,3,4},
            QuotaCategories = new List<string>() {"age"}
        };
        return result;
    }
    
    public static PassengerTypeDTO GetPassengerTypeDTO()
    {
        var result = new PassengerTypeDTO()
        {
            Code = "child",
            Type = "ребенок",
            Description = "Сопровождаемый ребенок до 12 лет с предоставлением места",
            Appendices = new List<short>() {1,3,4},
            QuotaCategories = new List<string>() {"age"}
        };
        return result;
    }
}