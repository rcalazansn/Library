using Library.Core.Application;
using Library.Domain.Enum;
using MediatR;

namespace Library.Application.Command.AddUser
{
    public class AddUserCommand : BaseCommand, IRequest<AddUserCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserTypeEnm { get; set; }
    }
}
