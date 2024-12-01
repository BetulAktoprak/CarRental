namespace CarRental.Core.Repositories;
public interface IGenericRepository<T> where T : class
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    Task DeleteByIdAsync(Guid id);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
}
