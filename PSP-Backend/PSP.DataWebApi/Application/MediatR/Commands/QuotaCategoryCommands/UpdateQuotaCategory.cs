using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.QuotaCategoryCommands;

public static class UpdateQuotaCategory
{
    public record Command(QuotaCategoryDTO objDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IQuotaCategoryRepository repository)
        {
            RuleFor(x => x.objDto.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор категории квоты не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IQuotaCategoryRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.UpdateAsync(mapper.Map<QuotaCategory>(request.objDto)));
        }
    }
}