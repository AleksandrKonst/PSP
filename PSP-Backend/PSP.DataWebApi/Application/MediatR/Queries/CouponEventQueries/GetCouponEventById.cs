using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.CouponEventQueries;

public static class GetCouponEventById
{
    public record Query(long Code) : IRequest<QueryResult>;
    
    public record QueryResult(CouponEventDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(ICouponEventRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор события купона не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(ICouponEventRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<CouponEventDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}