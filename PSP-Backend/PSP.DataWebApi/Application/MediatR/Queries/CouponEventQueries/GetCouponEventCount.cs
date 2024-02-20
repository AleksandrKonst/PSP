using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.CouponEventQueries;

public static class GetCouponEventCount
{
    public record Query : IRequest<QueryResult>;

    public record QueryResult(long Result);
    
    public class Handler(ICouponEventRepository repository) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(await repository.GetCountAsync());
        }
    }
}