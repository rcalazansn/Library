﻿using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Domain.Validations;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.AddBook
{
    public class AddBookCommandHandler : BaseCommandHandler,
        IRequestHandler<AddBookCommand, AddBookCommandResponse>
    {
        private readonly ILogger<AddBookCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public AddBookCommandHandler
        (
            ILogger<AddBookCommandHandler> logger,
            INotifier notifier,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<AddBookCommandResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var book = new Book(request.Title, request.Author, request.ISBN, request.YearOfPublication);

            if (!ExecuteValidation(new AddBookValidation(), book))
                return null;

            var bookDb = await _uow.BookRepository.FirstAsync(_ => _.Title.Equals(book.Title));

            if (bookDb != null)
            {
                Notify("Title already registered");
                return null;
            }

            _uow.BookRepository.Add(book);

            var success = await _uow.CommitAsync(cancellationToken);

            if (!success)
                return null;

            //publish (rabbitMQ)

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Title} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return new AddBookCommandResponse()
            {
                Id = book.Id,
                Author = request.Author,
                YearOfPublication = request.YearOfPublication,
                Title = request.Title,
                ISBN = request.ISBN
            };
        }
    }
}
