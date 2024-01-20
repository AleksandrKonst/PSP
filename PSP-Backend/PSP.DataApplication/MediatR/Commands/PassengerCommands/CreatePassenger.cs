using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.Mediatr.Commands.PassengerCommands;

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
            
            RuleFor(x => x.PassengerDto.DocumentTypeCode)
                .Length(2)
                .WithMessage("Номер документа больше 2 символов")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.PassengerDto.DocumentNumber)
                .MaximumLength(20)
                .WithMessage("Серия документа больше 20 символов")
                .WithErrorCode("PPC-000403");
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