using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.PassengerCommands;

public static class CreatePassenger
{
    public record Command(PassengerDTO PassengerDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPassengerRepository repository)
        {
            RuleFor(x => x.PassengerDto.Id)
                .MustAsync(async (id, cancellationToken) => await repository.CheckByIdAsync(id) == false)
                .WithMessage("Имеется повторяющийся идентификатор пассажира")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IPassengerRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<Passenger>(request.PassengerDto)));
        }
    }
}