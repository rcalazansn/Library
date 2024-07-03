using Library.Core.Infrastructure;
using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<LibraryDbContext, Book>,
        IBookRepository
    {
        public BookRepository(LibraryDbContext dbContext)
           : base(dbContext) { }
    }
}
