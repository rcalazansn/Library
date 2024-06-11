using Library.Domain.Models;

namespace Library.Domain.Repositories
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan);
        Task<List<Loan>> GetAsync();
        Task ReturnBookAsync(int id);
    }
}
