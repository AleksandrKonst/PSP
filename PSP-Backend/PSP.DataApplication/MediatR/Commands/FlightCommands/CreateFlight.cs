using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.FlightCommands;

public static class CreateFlight
{
    public record Command(FlightDTO FlightDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IFlightRepository repository)
        {
            RuleFor(x => x.FlightDto.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор перелета")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IFlightRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<Flight>(request.FlightDto)));
        }
    }
}