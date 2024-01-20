using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.PassengerQuotaCountCommands;

public static class DeletePassengerQuotaCount
{
    public record Command(long PassengerId) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.PassengerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Неверный формат данных");
        }
    }
    
    public class Handler(IPassengerQuotaCountRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteAsync(request.PassengerId));
        }
    }
}