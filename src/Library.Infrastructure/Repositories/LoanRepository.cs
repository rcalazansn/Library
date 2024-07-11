using Library.Core.Infrastructure;
using Library.Domain.Models;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository : GenericRepository<LibraryDbContext, Loan>, 
        ILoanRepository
    {
        private readonly LibraryDbContext _dbContext;
        public LoanRepository(LibraryDbContext dbContext)
            : base(dbContext) => _dbContext = dbContext;

        public async Task ReturnBookAsync(int id)
        {
            var loan = await _dbContext.Loans.SingleOrDefaultAsync(p => p.Id == id);

            if (loan != null)
            {
                var book = await _dbContext.Books.SingleOrDefaultAsync(p => p.Id == loan.BookId);

                if (book != null)
                {
                    loan.ReturnBook();

                    book.Available();
                }
            }
        }
    }
}
