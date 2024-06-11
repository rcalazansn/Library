using FluentValidation.Results;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repositories;

namespace Library.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private readonly ValidationResult _validationResult;
        public UnitOfWork(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

        public async Task<ValidationResult> Commit(CancellationToken cancellationToken)
        {
            _validationResult.Errors.Clear();

            var sucesso = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!sucesso)
                AddError("There was an error persisting data!");

            return _validationResult;
        }
        protected void AddError(string message) => _validationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        public void Dispose() => _context.Dispose();
    }
}
