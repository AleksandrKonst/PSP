using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.SubsidizedRouteQueries;

public static class GetSubsidizedRouteById
{
    public record Query(long Code) : IRequest<QueryResult>;
    
    public record QueryResult(SubsidizedDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(ISubsidizedRouteRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор субсидируемого направления не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(ISubsidizedRouteRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<SubsidizedDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}