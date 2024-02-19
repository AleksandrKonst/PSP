using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.QuotaCategoryQueries;

public static class QuotaCategoryById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(QuotaCategoryDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IQuotaCategoryRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор категории квоты не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IQuotaCategoryRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<QuotaCategoryDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}