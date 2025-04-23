using Library.Core.Notification;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private readonly INotifier _notifier;
        private IDbContextTransaction? _dbContextTransaction;
        public UnitOfWork(LibraryDbContext context, INotifier notifier)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        private IBookRepository? _bookRepository;
        private ILoanRepository? _loanRepository;
        private IUserRepository? _userRepository;
        public IBookRepository BookRepository =>
            _bookRepository ??= new BookRepository(_context);
        public ILoanRepository LoanRepository =>
            _loanRepository ??= new LoanRepository(_context);
        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);
        public async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            bool success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!success)
                _notifier.Handle(new Notication("no records were saved/changed/deleted!"));

            return success;
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_dbContextTransaction == null)
                _dbContextTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_dbContextTransaction != null)
            {
                await _context.SaveChangesAsync(cancellationToken);

                await _context.Database.CommitTransactionAsync(cancellationToken);

                await _dbContextTransaction.DisposeAsync();

                _dbContextTransaction = null;
            }
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_dbContextTransaction != null)
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken);

                await _dbContextTransaction.DisposeAsync();

                _dbContextTransaction = null;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContextTransaction?.Dispose();

                _context.Dispose();
            }
        }
    }
}
