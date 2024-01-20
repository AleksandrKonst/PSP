using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.DocumentTypeQueries;

public static class GetDocumentTypeById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(DocumentTypeDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Неверный формат данных");
        }
    }
    
    public class Handler(IDocumentTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<DocumentTypeDTO>(await repository.GetByIdAsync(request.Code)));
        }
    }
}