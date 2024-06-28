using Library.Core.Infrastructure;
using Library.Domain.Models;

namespace Library.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
       // Task AddAsync(User user);
       // Task<List<User>> GetAsync(string query);
    }
}
