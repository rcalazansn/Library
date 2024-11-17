using Library.Application.Notifications.Book;
using Library.Application.Notifications.Loan;
using Library.Application.Notifications.User;
using Library.Core.RabbitMQ;
using MediatR;
using Microsoft.Extensions.Options;

namespace Library.Application.Notifications
{
    public class LibraryEventHandler :
        INotificationHandler<UserCreatedNotification>,
        INotificationHandler<BookCreatedNotification>,
        INotificationHandler<BookRemovedNotification>,
        INotificationHandler<LoanCreatedNotification>,
        INotificationHandler<ReturnBookNotification>
    {
        private readonly RabbitMqSetting _rabbitMqSetting;

        private string SERVICE_NAME = typeof(LibraryEventHandler).Name;

        public LibraryEventHandler(IOptions<RabbitMqSetting> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
        }
        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"UserCreated: '{notification.Id} - {notification.Name} - {notification.Email}'");

                new RabbitPublisherBase(hostname: _rabbitMqSetting.HostName, port: _rabbitMqSetting.Port,
                        serviceName: SERVICE_NAME, userName: _rabbitMqSetting.UserName,
                        password: _rabbitMqSetting.Password)
                    .BuildPublish(notification, typeof(UserCreatedNotification).Name.ToString());
            });
        }
        public async Task Handle(BookCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"BookCreated: '{notification.Id} - {notification.Author} - {notification.Title}'");

                new RabbitPublisherBase(hostname: _rabbitMqSetting.HostName, port: _rabbitMqSetting.Port,
                        serviceName: SERVICE_NAME, userName: _rabbitMqSetting.UserName,
                        password: _rabbitMqSetting.Password)
                    .BuildPublish(notification, typeof(BookCreatedNotification).Name.ToString());
            });
        }
        public async Task Handle(BookRemovedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"BookRemoved: '{notification.Id} - {notification.Author} - {notification.Title}'");

                new RabbitPublisherBase(hostname: _rabbitMqSetting.HostName, port: _rabbitMqSetting.Port,
                        serviceName: SERVICE_NAME, userName: _rabbitMqSetting.UserName,
                        password: _rabbitMqSetting.Password)
                    .BuildPublish(notification, typeof(BookRemovedNotification).Name.ToString());
            });
        }
        public async Task Handle(LoanCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"LoanCreated: '{notification.Id} - {notification.UserId} - {notification.BookId}'");

                new RabbitPublisherBase(hostname: _rabbitMqSetting.HostName, port: _rabbitMqSetting.Port,
                        serviceName: SERVICE_NAME, userName: _rabbitMqSetting.UserName,
                        password: _rabbitMqSetting.Password)
                    .BuildPublish(notification, typeof(LoanCreatedNotification).Name.ToString());
            });
        }
        public async Task Handle(ReturnBookNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"ReturnBook: '{notification.Id}'");

                new RabbitPublisherBase(hostname: _rabbitMqSetting.HostName, port: _rabbitMqSetting.Port,
                        serviceName: SERVICE_NAME, userName: _rabbitMqSetting.UserName,
                        password: _rabbitMqSetting.Password)
                    .BuildPublish(notification, typeof(ReturnBookNotification).Name.ToString());
            });
        }
    }
}