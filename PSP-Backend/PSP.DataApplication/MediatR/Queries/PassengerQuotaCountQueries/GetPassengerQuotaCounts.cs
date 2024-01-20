using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.PassengerQuotaCountQueries;

public static class GetPassengerQuotaCounts
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<PassengerQuotaCountDTO> Result);
    
    public class Handler(IPassengerQuotaCountRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<PassengerQuotaCountDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}