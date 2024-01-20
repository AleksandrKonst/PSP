using MediatR;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.Mediatr.Queries.PassengerQueries;

public static class GetPassengerCount
{
    public record Query : IRequest<QueryResult>;

    public record QueryResult(int Result);
    
    public class Handler(IPassengerRepository repository) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(await repository.GetCountAsync());
        }
    }
}