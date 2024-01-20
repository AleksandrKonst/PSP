using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.PassengerQuotaCountCommands;

public static class CreatePassengerQuotaCount
{
    public record Command(PassengerQuotaCountDTO PassengerQuotaCountDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.PassengerQuotaCountDto.PassengerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Неверный формат данных");
        }
    }
    
    public class Handler(IPassengerQuotaCountRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<PassengerQuotaCount>(request.PassengerQuotaCountDto)));
        }
    }
}