using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Domain.Exceptions;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.Mediatr.Queries.PassengerQueries;

public static class GetPassengerById
{
    public record Query(int Id) : IRequest<QueryResult>;
    
    public record QueryResult(PassengerDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IPassengerRepository repository)
        {
            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellationToken) => await repository.CheckByIdAsync(id))
                .WithMessage("Идентификатор пассажира не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IPassengerRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var passenger = await repository.GetByIdAsync(request.Id);
            return new QueryResult(mapper.Map<PassengerDTO>(passenger));
        }
    }
}