using Library.Application.Notifications.Book;
using Library.Application.Notifications.Loan;
using Library.Application.Notifications.User;
using Library.Core.RabbitMQ;
using MediatR;

namespace Library.Application.Notifications
{
    public class LibraryEventHandler :
            INotificationHandler<UserCreatedNotification>,
            INotificationHandler<BookCreatedNotification>,
            INotificationHandler<BookRemovedNotification>,
            INotificationHandler<LoanCreatedNotification>,
            INotificationHandler<ReturnBookNotification>
    {

        private string SERVICE_NAME = typeof(LibraryEventHandler).Name;
        private const string USER_NAME = "library";
        private const string PASSWORD = "library!@#";
        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"UserCreated: '{notification.Id} - {notification.Name} - {notification.Email}'");

                new RabbitPublisherBase(serviceName: SERVICE_NAME, userName: USER_NAME, password: PASSWORD)
                    .BuildPublish(notification, typeof(UserCreatedNotification).Name.ToString());
            });
        }

        public async Task Handle(BookCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"BookCreated: '{notification.Id} - {notification.Author} - {notification.Title}'");

                new RabbitPublisherBase(serviceName: SERVICE_NAME, userName: USER_NAME, password: PASSWORD)
                    .BuildPublish(notification, typeof(BookCreatedNotification).Name.ToString());
            });
        }

        public async Task Handle(BookRemovedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"BookRemoved: '{notification.Id} - {notification.Author} - {notification.Title}'");

                new RabbitPublisherBase(serviceName: SERVICE_NAME, userName: USER_NAME, password: PASSWORD)
                    .BuildPublish(notification, typeof(BookRemovedNotification).Name.ToString());
            });
        }
        public async Task Handle(LoanCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"LoanCreated: '{notification.Id} - {notification.UserId} - {notification.BookId}'");

                new RabbitPublisherBase(serviceName: SERVICE_NAME, userName: USER_NAME, password: PASSWORD)
                    .BuildPublish(notification, typeof(LoanCreatedNotification).Name.ToString());
            });
        }
        public async Task Handle(ReturnBookNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"ReturnBook: '{notification.Id}'");

                new RabbitPublisherBase(serviceName: SERVICE_NAME, userName: USER_NAME, password: PASSWORD)
                   .BuildPublish(notification, typeof(ReturnBookNotification).Name.ToString());
            });
        }
    }
}
