using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.FlightCommands;

public static class DeleteFlight
{
    public record Command(long code) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IFlightRepository repository)
        {
            RuleFor(x => x.code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор перелета не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IFlightRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteAsync(request.code));
        }
    }
}