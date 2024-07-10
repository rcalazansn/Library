using Library.Application.Notifications.book;
using Library.Application.Notifications.User;
using MediatR;

namespace Library.Application.Notifications
{
    public class LibraryEventHandler :
            INotificationHandler<UserCreatedNotification>,
            INotificationHandler<BookCreatedNotification>,
            INotificationHandler<BookRemovedNotification>
    {
        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"UserCreated: '{notification.Id} - {notification.Name} - {notification.Email}'");
            });
        }

        public async Task Handle(BookCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"BookCreated: '{notification.Id} - {notification.Author} - {notification.Title}'");
            });
        }

        public async Task Handle(BookRemovedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"BookRemoved: '{notification.Id} - {notification.Author} - {notification.Title}'");
            });
        }
    }
}
