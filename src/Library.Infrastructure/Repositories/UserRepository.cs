using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _dbContext;
        public UserRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> GetAsync(string query)
        {
            var usuarios = await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();

            return usuarios;
        }

        public async Task SaveAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }
    }
}
