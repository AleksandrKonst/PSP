using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IDocumentTypeRepository
{
    Task<List<DocumentType>> GetAllAsync();
    Task<List<DocumentType>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<DocumentType> GetByIdAsync(string id);
    Task<int> GetCountAsync();
    Task<bool> AddAsync(DocumentType documentType);
    Task<bool> UpdateAsync(DocumentType documentType);
    Task<bool> DeleteAsync(string code);
}