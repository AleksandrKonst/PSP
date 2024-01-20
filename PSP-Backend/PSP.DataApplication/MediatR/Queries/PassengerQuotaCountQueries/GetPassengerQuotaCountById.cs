using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.PassengerQuotaCountQueries;

public static class GetPassengerQuotaCountById
{
    public record Query(long PassengerId, string QuotaCategory, int Year) : IRequest<QueryResult>;
    
    public record QueryResult(PassengerQuotaCountDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.PassengerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Неверный формат данных");
            
            RuleFor(x => x.QuotaCategory)
                .NotEmpty()
                .Length(20)
                .WithMessage("Неверный формат данных");
        }
    }
    
    public class Handler(IPassengerQuotaCountRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<PassengerQuotaCountDTO>(await repository.GetByIdAsync(request.PassengerId, request.QuotaCategory, request.Year)));
        }
    }
}