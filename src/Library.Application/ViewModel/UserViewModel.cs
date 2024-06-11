using Library.Domain.Enum;
using Library.Domain.Utils;

namespace Library.Application.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel(int id, string name, string email, UserTypeEnum userTypeEnum)
        {
            Id = id;
            Name = name;
            Email = email;
            UserTypeEnum = userTypeEnum.GetDisplayName();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string UserTypeEnum { get; private set; }
    }
}
