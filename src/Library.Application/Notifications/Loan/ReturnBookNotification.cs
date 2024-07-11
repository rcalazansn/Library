using MediatR;

namespace Library.Application.Notifications.Loan
{
    public class ReturnBookNotification : INotification
    {
        public int Id { get; private set; }
    }
}
