using Application.DTO.FlightContextDTO;
using AutoMapper;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.SubsidizedRouteQueries;

public static class GetSubsidizedRoutes
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<SubsidizedDTO> Result);
    
    public class Handler(ISubsidizedRouteRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<SubsidizedDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}