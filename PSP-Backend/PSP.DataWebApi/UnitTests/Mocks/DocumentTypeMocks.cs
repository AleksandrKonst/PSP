using Application.DTO.PassengerContextDTO;
using Domain.Models;

namespace UnitTests.Mocks;

public class DocumentTypeMocks
{
    public static DocumentType GetDocumentType()
    {
        var result = new DocumentType()
        {
            Code = "00",
            Type = "Паспорт гражданина Российской Федерации"
        };
        return result;
    }
    
    public static DocumentTypeDTO GetDocumentTypeDTO()
    {
        var result = new DocumentTypeDTO()
        {
            Code = "00",
            Type = "Паспорт гражданина Российской Федерации"
        };
        return result;
    }
}