using AutoMapper;
using MediatR;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.PassengerTypeQueries;

public static class GetPassengerTypes
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<PassengerTypeDTO> Result);
    
    public class Handler(IPassengerTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<PassengerTypeDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}