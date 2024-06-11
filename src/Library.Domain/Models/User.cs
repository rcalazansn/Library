using Library.Domain.Enum;

namespace Library.Domain.Models
{
    public class User : BaseEntity
    {
        public User(string name, string email, UserTypeEnm userTypeEnm)
        {
            Name = name;
            Email = email;
            UserTypeEnm = userTypeEnm;
            Loans = new List<Loan> { };
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserTypeEnm UserTypeEnm { get; private set; }
        public List<Loan> Loans { get; private set; }
    }
}
