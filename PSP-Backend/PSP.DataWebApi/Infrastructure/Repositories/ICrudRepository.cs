namespace Infrastructure.Repositories;

public interface ICrudRepository <T, C>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<T?> GetByCodeAsync(C code);
    Task<long> GetCountAsync();
    Task<bool> CheckByCodeAsync(C code);
    Task<bool> AddAsync(T obj);
    Task<bool> UpdateAsync(T obj);
    Task<bool> DeleteAsync(C code);
}