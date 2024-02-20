using Application.DTO.FlightContextDTO;
using AutoMapper;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.CouponEventQueries;

public static class GetCouponEvents
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<CouponEventDTO> Result);
    
    public class Handler(ICouponEventRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<CouponEventDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}