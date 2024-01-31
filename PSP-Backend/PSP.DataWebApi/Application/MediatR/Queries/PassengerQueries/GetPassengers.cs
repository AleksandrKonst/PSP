using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.PassengerQueries;

public static class GetPassengers
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<PassengerDTO> Result);
    
    public class Handler(IPassengerRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<PassengerDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}