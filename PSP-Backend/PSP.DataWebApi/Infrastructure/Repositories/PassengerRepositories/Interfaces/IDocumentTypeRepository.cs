using Domain.Models;

namespace Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IDocumentTypeRepository
{
    Task<IEnumerable<DocumentType>> GetAllAsync();
    Task<IEnumerable<DocumentType>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<DocumentType?> GetByCodeAsync(string code);
    Task<int> GetCountAsync();
    Task<bool> CheckByCodeAsync(string code);
    Task<bool> AddAsync(DocumentType documentType);
    Task<bool> UpdateAsync(DocumentType documentType);
    Task<bool> DeleteAsync(string code);
}