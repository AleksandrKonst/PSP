using Application.DTO.ArmContextDTO.Delete;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.ARMCommands;

public static class DeleteCouponEvent
{
    public record Command(DeleteCouponEventRequestDTO DeleteCouponEventRequestDto) : IRequest<CommandResult>;
    
    public record CommandResult(int Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ICouponEventRepository repository)
        {
            RuleFor(x => x.DeleteCouponEventRequestDto.TicketType)
                .LessThan(100)
                .WithMessage("Неверный формат типа билета")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.DeleteCouponEventRequestDto.TicketNumber)
                .MaximumLength(30)
                .WithMessage("Слишком длинный номер билета")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(ICouponEventRepository repository) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteByTicketAsync(request.DeleteCouponEventRequestDto.OperationType, 
                request.DeleteCouponEventRequestDto.TicketType, 
                request.DeleteCouponEventRequestDto.TicketNumber, 
                DateTime.Parse(request.DeleteCouponEventRequestDto.OperationDatetime).ToUniversalTime()));
        }
    }
}