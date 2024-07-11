using Library.Core.Infrastructure;
using Library.Domain.Models;

namespace Library.Domain.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task ReturnBookAsync(int id);
    }
}
