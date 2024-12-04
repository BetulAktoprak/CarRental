using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using FluentValidation;

namespace CarRental.Business.Services;
public class Service<T> : IService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<T> _validator;

    public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IValidator<T> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task AddAsync(T entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);

        if (!validationResult.IsValid)
        {
            throw new Exception(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

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
        var validationResult = await _validator.ValidateAsync(entity);

        if (!validationResult.IsValid)
        {
            throw new Exception(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        _repository.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}
