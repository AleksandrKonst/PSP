using Application.DTO.FlightContextDTO;
using AutoMapper;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.QuotaCategoryQueries;

public static class GetQuotaCategories
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<QuotaCategoryDTO> Result);
    
    public class Handler(IQuotaCategoryRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<QuotaCategoryDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}