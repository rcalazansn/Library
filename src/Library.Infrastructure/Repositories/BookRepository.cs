using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;
        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Book>> GetAsync(string query)
        {
            var livrosQuery = _dbContext.Books
               .Include(e => e.Loans)
               .AsNoTracking();

            if (!string.IsNullOrEmpty(query))
            {
                Expression<Func<Book, bool>> filter = l => l.Title.Contains(query) || l.Author.Contains(query);

                livrosQuery = livrosQuery.Where(filter);
            }

            var livros = await livrosQuery.ToListAsync();

            return livros;
        }

        public async Task<List<Book>> GetAsync()
        {
            var livros = await _dbContext.Books.AsNoTracking()
                .Where(t => t.Status == Domain.Enum.BookStatusEnum.Available)
                .ToListAsync();

            return livros;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var livro = await _dbContext.Books.SingleOrDefaultAsync(t => t.Id == id);

            return livro;
        }

        public async Task RemoveAsync(int id)
        {
            var livro = await _dbContext.Books.SingleOrDefaultAsync(p => p.Id == id);

            if (livro != null)
            {
                _dbContext.Books.Remove(livro);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> SaveAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);

            await _dbContext.SaveChangesAsync();

            return book.Id;
        }
    }
}
