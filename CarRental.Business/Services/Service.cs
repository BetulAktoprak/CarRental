using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;

namespace CarRental.Business.Services;
public class Service<T> : IService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(T entity)
    {
        await _repository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        try
        {
            await _repository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task Update(T entity)
    {
        _repository.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}
