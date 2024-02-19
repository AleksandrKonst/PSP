using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.TicketTypeQueries;

public static class TicketTypeById
{
    public record Query(short Code) : IRequest<QueryResult>;
    
    public record QueryResult(TicketTypeDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(ITicketTypeRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор типа билета не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(ITicketTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<TicketTypeDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}