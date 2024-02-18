using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.FareQueries;

public static class GetFareEventById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(FareDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IFareRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор тарифа не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IFareRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<FareDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}