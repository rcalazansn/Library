namespace Library.Core.Notification
{
    public interface INotifier
    {
        bool HasNotification();
        IList<Notication> GetNotifications();
        void Handle(Notication notificacao);
    }
}
