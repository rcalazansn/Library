using Library.Domain.Enum;

namespace Library.Domain.Models
{
    public class User : BaseEntity
    {
        public User(string name, string email, UserTypeEnum userTypeEnum)
        {
            Name = name;
            Email = email;
            UserTypeEnum = userTypeEnum;
            Loans = new List<Loan> { };
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserTypeEnum UserTypeEnum { get; private set; }
        public List<Loan> Loans { get; private set; }
    }
}
