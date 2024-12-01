using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.DataAccess.Context;

namespace CarRental.DataAccess.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
