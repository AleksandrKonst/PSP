using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.PassengerTypeCommands;

public static class CreatePassengerType
{
    public record Command(PassengerTypeDTO PassengerTypeDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPassengerTypeRepository repository)
        {
            RuleFor(x => x.PassengerTypeDto.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор типа пассажира")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IPassengerTypeRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<PassengerType>(request.PassengerTypeDto)));
        }
    }
}