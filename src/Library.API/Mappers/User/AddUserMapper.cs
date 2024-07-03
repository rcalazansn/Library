using Library.API.Dtos.User;
using Library.Application.Command.AddUser;

namespace Library.API.Mappers.User
{
    public static class AddUserMapper
    {
        public static AddUserCommand MapToAddUserCommand(this AddUserRequestDto dto)
        {
            return new AddUserCommand()
            {
                Name = dto.Name,
                Email = dto.Email,
                UserTypeEnm = dto.UserTypeEnm,
            };
        }
    }
}
