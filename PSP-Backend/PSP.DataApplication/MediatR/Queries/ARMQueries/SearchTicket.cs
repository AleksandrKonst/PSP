using System.Dynamic;
using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.ArmContextDTO.Search;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.ARMQueries;

public static class SearchTicket
{
   public record Query(SearchTicketDTO SearchTicketDto) : IRequest<QueryResult>;
    
    public record QueryResult(dynamic Result);
    
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            
        }
    }
    
    public class Handler(ICouponEventRepository couponEventRepository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            dynamic coupon = new ExpandoObject();
            coupon.couponEvents = mapper.Map<List<CouponEventDTO>>(await couponEventRepository.GetAllAsync(request.SearchTicketDto.TicketType, request.SearchTicketDto.TicketNumber));

            return new QueryResult(coupon);
        }
    }
}