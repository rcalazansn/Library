using Library.Domain.Repositories;

namespace Library.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        IBookRepository BookRepository { get; }
        ILoanRepository LoanRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
