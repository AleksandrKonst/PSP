using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.DocumentTypeQueries;

public static class GetDocumentTypeCount
{
    public record Query : IRequest<QueryResult>;

    public record QueryResult(int Result);
    
    public class Handler(IDocumentTypeRepository repository) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(await repository.GetCountAsync());
        }
    }
}