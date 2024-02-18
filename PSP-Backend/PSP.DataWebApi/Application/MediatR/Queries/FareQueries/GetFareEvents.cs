using Application.DTO.FlightContextDTO;
using AutoMapper;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.FareQueries;

public static class GetFareEvents
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<FareDTO> Result);
    
    public class Handler(IFareRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<FareDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}