using MediatR;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.PassengerTypeQueries;

public static class GetPassengerTypeCount
{
    public record Query : IRequest<QueryResult>;

    public record QueryResult(int Result);
    
    public class Handler(IPassengerTypeRepository repository) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(await repository.GetCountAsync());
        }
    }
}