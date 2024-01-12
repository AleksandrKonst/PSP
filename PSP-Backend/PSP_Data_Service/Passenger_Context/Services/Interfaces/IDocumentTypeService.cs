using PSP_Data_Service.Passenger_Context.DTO;

namespace PSP_Data_Service.Passenger_Context.Services.Interfaces;

public interface IDocumentTypeService
{
    Task<IEnumerable<DocumentTypeDTO>> GetDocumentTypesAsync(int index, int count);
    Task<int> GetDocumentTypesCountAsync();
    Task<DocumentTypeDTO> GetDocumentTypeByIdAsync(string id);
    Task<bool> AddDocumentTypeAsync(DocumentTypeDTO dto);
    Task<bool> UpdateDocumentTypeAsync(DocumentTypeDTO dto);
    Task<bool> DeleteDocumentTypeAsync(string id);
}