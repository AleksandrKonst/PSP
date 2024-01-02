using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

public interface IDocumentTypeRepository
{
    IQueryable<DocumentType> GetAll();
    Task<DocumentType> GetByIdAsync(string code);
    Task<bool> Add(DocumentType documentType);
    Task<bool> Update(DocumentType documentType);
    Task<bool> Delete(string code);
}