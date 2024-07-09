using Library.Domain.Enum;
using MediatR;

namespace Library.Application.Notifications.User
{
    public class UserCreatedNotification : INotification
    {
        public UserCreatedNotification(int id, string name, string email, UserTypeEnum userTypeEnm)
        {
            Id = id;
            Name = name;
            Email = email;
            UserTypeEnm = userTypeEnm;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserTypeEnum UserTypeEnm { get; private set; }
    }
}
