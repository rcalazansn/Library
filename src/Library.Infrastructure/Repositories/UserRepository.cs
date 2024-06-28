using Library.Core.Infrastructure;
using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<LibraryDbContext, User>, 
        IUserRepository
    {
       // private readonly LibraryDbContext _dbContext;
        public UserRepository(LibraryDbContext dbContext)
            : base(dbContext)
        {
           // _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        //public async Task<List<User>> GetAsync(string query)
        //{
        //    var usuarios = await _dbContext.Users
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return usuarios;
        //}

        //public async Task AddAsync(User user)
        //{
        //    await _dbContext.Users.AddAsync(user);
        //}
    }
}
