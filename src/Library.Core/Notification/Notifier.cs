namespace Library.Core.Notification
{
    public class Notifier : INotifier
    {
        private IList<Notication> _notifications;
        public Notifier()
        {
            _notifications = new List<Notication>();
        }
        public IList<Notication> GetNotifications() => _notifications;

        public void Handle(Notication notificacao) => _notifications.Add(notificacao);

        public bool HasNotification() => _notifications.Any();
    }
}
