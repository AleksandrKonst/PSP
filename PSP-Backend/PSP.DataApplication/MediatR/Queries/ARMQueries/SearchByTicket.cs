using System.Dynamic;
using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.ArmContextDTO.Search;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.ARMQueries;

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
    
    public class Handler(ICouponEventRepository couponEventRepository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var coupon = new SearchTicketResponseDTO()
            {
                CouponEvents = mapper.Map<List<CouponEventDTO>>(await couponEventRepository.GetAllAsync(request.SearchByTicketDto.TicketType, request.SearchByTicketDto.TicketNumber))
            };
            return new QueryResult(coupon);
        }
    }
}