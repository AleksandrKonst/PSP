using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.FareCommands;

public static class CreateFare
{
    public record Command(FareDTO FareDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IFareRepository repository)
        {
            RuleFor(x => x.FareDto.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор тарифа")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IFareRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<Fare>(request.FareDto)));
        }
    }
}