using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.PassengerTypeCommands;

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