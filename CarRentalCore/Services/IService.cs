namespace CarRental.Core.Services;
public interface IService<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task Update(T entity);
    Task<bool> DeleteByIdAsync(Guid id);
}
