using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.Mediatr.Commands.PassengerCommands;

public static class DeletePassenger
{
    public record Command(long Id) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPassengerRepository repository)
        {
            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellationToken) => await repository.CheckByIdAsync(id))
                .WithMessage("Идентификатор пассажира не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IPassengerRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteAsync(request.Id));
        }
    }
}