using Library.Core.Notification;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repositories;

namespace Library.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private readonly INotifier _notifier;
        public UnitOfWork(LibraryDbContext context, INotifier notifier)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        private IBookRepository _bookRepository;
        public IBookRepository BookRepository
        {
            get => _bookRepository ?? (_bookRepository = new BookRepository(_context));
        }

        private ILoanRepository _loanRepository;
        public ILoanRepository LoanRepository
        {
            get => _loanRepository ?? (_loanRepository = new LoanRepository(_context));
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get => _userRepository ?? (_userRepository = new UserRepository(_context));
        }
        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            bool success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!success)
                _notifier.Handle(new Notication("no records were saved/changed/deleted!"));

            return success;
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
                _context.Dispose();
            }
        }
    }
}
