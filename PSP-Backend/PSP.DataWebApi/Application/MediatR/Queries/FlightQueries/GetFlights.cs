using Application.DTO.FlightContextDTO;
using AutoMapper;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.FlightQueries;

public static class GetFlights
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<FlightDTO> Result);
    
    public class Handler(IFlightRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<FlightDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}