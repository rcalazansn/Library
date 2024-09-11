namespace Library.Core.Domain
{
    public interface ISoftDelete
    {
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
