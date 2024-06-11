using Library.Domain.Models;

namespace Library.Domain.Repositories
{
    public interface IUserRepository
    {
        Task SaveAsync(User user);
        Task<List<User>> GetAsync(string query);
    }
}
