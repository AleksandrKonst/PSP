using Application.DTO.ArmContextDTO.Search;
using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Queries.ARMQueries;

public static class SearchByTicket
{
    public record Query(SearchByTicketDTO SearchByTicketDto) : IRequest<QueryResult>;
    
    public record QueryResult(SearchTicketResponseDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.SearchByTicketDto.TicketType)
                .LessThan(100)
                .WithMessage("Неверный формат типа билета")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByTicketDto.TicketNumber)
                .MaximumLength(30)
                .WithMessage("Слишком длинный номер билета")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(ICouponEventRepository couponEventRepository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var coupon = new SearchTicketResponseDTO()
            {
                CouponEvents = mapper.Map<List<CouponEventDTO>>(await couponEventRepository.GetAllAsync(request.SearchByTicketDto.TicketType, request.SearchByTicketDto.TicketNumber))
            };
            logger.LogInformation($"Search {nameof(SearchByTicket)}");
            return new QueryResult(coupon);
        }
    }
}