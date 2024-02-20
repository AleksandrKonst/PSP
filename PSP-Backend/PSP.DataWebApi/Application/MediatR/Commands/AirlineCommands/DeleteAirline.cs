using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.AirlineCommands;

public static class DeleteAirline
{
    public record Command(string code) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IAirportRepository repository)
        {
            RuleFor(x => x.code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор перевозчика не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IAirportRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteAsync(request.code));
        }
    }
}