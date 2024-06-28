namespace Library.Core.Notification
{
    public class Notication
    {
        public Notication(string message) => Message = message;
        public string? Message { get; }

        public override string ToString() =>
            $"{Message}";
    }
}
