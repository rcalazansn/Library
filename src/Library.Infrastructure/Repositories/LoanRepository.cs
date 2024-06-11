using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _dbContext;
        public LoanRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<Loan>> GetAsync()
        {
            var emprestimos = await _dbContext.Loans
                .Include(e => e.User)
                .Include(e => e.Book)
                .AsNoTracking()
                .ToListAsync();

            return emprestimos;
        }
        public async Task ReturnBookAsync(int id)
        {
            var loan = await _dbContext.Loans.SingleOrDefaultAsync(p => p.Id == id);

            if (loan != null)
            {
                var book = await _dbContext.Books.SingleOrDefaultAsync(p => p.Id == loan.BookId);

                if (book != null)
                {
                    loan.DevolverLivro();

                    book.Available();
                }
            }
        }
        public async Task AddAsync(Loan loan)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(l => l.Id == loan.BookId);
            if (book == null)
                return;

            var user = await _dbContext.Users.AnyAsync(u => u.Id == loan.UserId);
            if (!user)
                return;

            book.Borrowed();

            await _dbContext.Loans.AddAsync(loan);
        }
    }
}
