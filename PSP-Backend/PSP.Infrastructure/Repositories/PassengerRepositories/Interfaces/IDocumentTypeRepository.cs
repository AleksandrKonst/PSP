using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IDocumentTypeRepository
{
    IQueryable<DocumentType> GetAll();
    Task<DocumentType> GetByIdAsync(string code);
    Task<bool> Add(DocumentType documentType);
    Task<bool> Update(DocumentType documentType);
    Task<bool> Delete(string code);
}