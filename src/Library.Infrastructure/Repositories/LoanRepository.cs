using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _dbContext;
        public LoanRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<string> ReturnBookAsync(int id)
        {
            var loan = await _dbContext.Loans.SingleOrDefaultAsync(p => p.Id == id);

            if (loan != null)
            {
                var book = await _dbContext.Books.SingleOrDefaultAsync(p => p.Id == loan.BookId);

                if (book != null)
                {
                    loan.DevolverLivro();

                    book.Available();
                    
                    await _dbContext.SaveChangesAsync();

                    if (loan.ReturnDate.Date > loan.DeadlineReturnDate.Date)
                    {
                        var daysLate = (loan.ReturnDate.Date - loan.DeadlineReturnDate.Date);

                        return "Return made successfully, but " + daysLate.TotalDays + " days late.";
                    }

                    return "Return completed successfully";
                }
            }

            return "Unable to complete return of book!";
        }

        public async Task SaveAsync(Loan loan)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(l => l.Id == loan.BookId);
            if (book == null)
            {
                throw new Exception($"BookId:{loan.BookId} not found!");
            }

            var user = await _dbContext.Users.AnyAsync(u => u.Id == loan.UserId);
            if (!user)
            {
                throw new Exception($"UsuarioId: {loan.UserId} not found!.");
            }

            book.Borrowed();

            await _dbContext.Loans.AddAsync(loan);
            await _dbContext.SaveChangesAsync();
        }
    }
}
