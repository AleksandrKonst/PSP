using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface ICouponEventRepository
{
    Task<bool> CheckByIdAsync(long id);

    Task<bool> AddAsync(CouponEvent couponEvent);
}