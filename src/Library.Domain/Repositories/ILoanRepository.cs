using Library.Domain.Models;

namespace Library.Domain.Repositories
{
    public interface ILoanRepository
    {
        Task SaveAsync(Loan loan);
        Task<List<Loan>> GetAsync();
        Task<string> ReturnBookAsync(int id);
    }
}
