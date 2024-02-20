using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.GenderTypeQueries;

public static class GetGenderTypeCount
{
    public record Query : IRequest<QueryResult>;

    public record QueryResult(long Result);
    
    public class Handler(IGenderTypeRepository repository) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(await repository.GetCountAsync());
        }
    }
}