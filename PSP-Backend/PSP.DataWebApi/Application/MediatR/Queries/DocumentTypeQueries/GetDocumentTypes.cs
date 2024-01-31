using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.DocumentTypeQueries;

public static class GetDocumentTypes
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<DocumentTypeDTO> Result);
    
    public class Handler(IDocumentTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<DocumentTypeDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}