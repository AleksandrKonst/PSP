using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.SubsidizedRouteQueries;

public class GetSubsidizedByAppendix
{
    public record Query(short Appendix) : IRequest<QueryResult>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Appendix)
                .LessThan((short) 6)
                .GreaterThan((short)0)
                .WithMessage("Неверный индекс приложения")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public record QueryResult(IEnumerable<SubsidizedDTO> Result);
    
    public class Handler(ISubsidizedRouteRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<SubsidizedDTO>>(await repository.GetAllByAppendixAsync(request.Appendix)));
        }
    }
}