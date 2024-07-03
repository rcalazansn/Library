using Library.Core.Infrastructure;
using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;

namespace Library.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<LibraryDbContext, User>,
        IUserRepository
    {
        public UserRepository(LibraryDbContext dbContext)
            : base(dbContext) { }
    }
}
