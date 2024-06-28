using System.ComponentModel.DataAnnotations;

namespace Library.Core.Application
{
    public abstract class BaseCommand
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        protected BaseCommand() => Timestamp = DateTime.Now;
    }
}
