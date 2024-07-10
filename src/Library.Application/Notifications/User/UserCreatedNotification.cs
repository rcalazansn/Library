using Library.Domain.Enum;
using MediatR;

namespace Library.Application.Notifications.User
{
    public class UserCreatedNotification : INotification
    {
        public UserCreatedNotification(Domain.Models.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            UserTypeEnm = user.UserTypeEnum;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserTypeEnum UserTypeEnm { get; private set; }
    }
}
