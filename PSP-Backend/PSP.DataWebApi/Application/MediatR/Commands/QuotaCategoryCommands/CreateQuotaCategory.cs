using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.QuotaCategoryCommands;

public static class CreateQuotaCategory
{
    public record Command(QuotaCategoryDTO objDTO) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IQuotaCategoryRepository repository)
        {
            RuleFor(x => x.objDTO.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор категории квоты")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IQuotaCategoryRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<QuotaCategory>(request.objDTO)));
        }
    }
}