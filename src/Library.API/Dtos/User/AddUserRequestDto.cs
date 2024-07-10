using Library.Domain.Enum;

namespace Library.API.Dtos.User
{
    public class AddUserRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserTypeEnm { get; set; }
    }
}
