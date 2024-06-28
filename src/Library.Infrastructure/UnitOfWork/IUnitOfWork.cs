using FluentValidation.Results;
using Library.Domain.Repositories;

namespace Library.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken);
        IBookRepository BookRepository { get; }
        ILoanRepository LoanRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
