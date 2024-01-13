using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP.DataWebApi.Passenger_Context.DTO;
using PSP.DataWebApi.Passenger_Context.Services.Interfaces;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataWebApi.Passenger_Context.Services;

public class DocumentTypeService(IDocumentTypeRepository repository, IMapper mapper) : IDocumentTypeService
{
    public async Task<IEnumerable<DocumentTypeDTO>> GetDocumentTypesAsync(int index, int count) => mapper.Map<IEnumerable<DocumentTypeDTO>>(await repository.GetAll().Skip(index).Take(count).ToListAsync());

    public async Task<int> GetDocumentTypesCountAsync() => await repository.GetAll().CountAsync();

    public async Task<DocumentTypeDTO> GetDocumentTypeByIdAsync(string id) => mapper.Map<DocumentTypeDTO>(await repository.GetByIdAsync(id));

    public async Task<bool> AddDocumentTypeAsync(DocumentTypeDTO dto) =>  await repository.Add(mapper.Map<DocumentType>(dto));

    public async Task<bool> UpdateDocumentTypeAsync(DocumentTypeDTO dto) =>  await repository.Update(mapper.Map<DocumentType>(dto));

    public async Task<bool> DeleteDocumentTypeAsync(string id) =>  await repository.Delete(id);
}