using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.DocumentTypeCommands;

public static class DeleteDocumentType
{
    public record Command(string Code) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IDocumentTypeRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор типа документа не существует")
                .WithErrorCode("PPC-000001");
            
            RuleFor(x => x.Code)
                .MaximumLength(2)
                .WithMessage("Идентификатор типа документа слишком длинный")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IDocumentTypeRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteAsync(request.Code));
        }
    }
}