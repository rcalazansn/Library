using Library.API.Dtos.User;
using Library.Application.Command.AddUser;

namespace Library.API.Mappers.User
{
    public static class AddUserMapper
    {
        public static AddUserCommand MapToAddUserCommand(this AddUserRequestDto request)
        {
            return new AddUserCommand()
            {
                Name = request.Name,
                Email = request.Email,
                UserTypeEnm = request.UserTypeEnm,
            };
        }
    }
}
