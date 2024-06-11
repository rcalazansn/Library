using Library.Domain.Enum;
using MediatR;

namespace Library.Application.Command.AddUser
{
    public class AddUserCommand : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserTypeEnm { get; set; }
    }
}
