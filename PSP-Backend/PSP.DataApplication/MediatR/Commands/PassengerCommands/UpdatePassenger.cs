using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.Mediatr.Commands.PassengerCommands;

public static class UpdatePassenger
{
    public record Command(PassengerDTO PassengerDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPassengerRepository repository)
        {
            RuleFor(x => x.PassengerDto.Id)
                .MustAsync(async (id, cancellationToken) => await repository.CheckByIdAsync(id))
                .WithMessage("Идентификатор пассажира не существует")
                .WithErrorCode("PPC-000001");
            
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
            return new CommandResult(await repository.UpdateAsync(mapper.Map<Passenger>(request.PassengerDto)));
        }
    }
}