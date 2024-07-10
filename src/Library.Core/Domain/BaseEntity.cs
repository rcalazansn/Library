namespace Library.Core.Domain
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { }
        public int Id { get; private set; }
        public DateTime CreateddAt { get; private set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; private set; } = null;
    }
}
