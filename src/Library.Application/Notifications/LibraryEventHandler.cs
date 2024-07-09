using Library.Application.Notifications.User;
using MediatR;

namespace Library.Application.Notifications
{
    public class LibraryEventHandler :
            INotificationHandler<UserCreatedNotification>
    {
        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"USER: '{notification.Id} - {notification.Name} - {notification.Email}'");
            });
        }
    }
}
