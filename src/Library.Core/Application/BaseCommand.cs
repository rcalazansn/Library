using System.ComponentModel.DataAnnotations;

namespace Library.Core.Application
{
    public abstract class BaseCommand
    {
        protected DateTime Timestamp { get; private set; }
        protected BaseCommand() => Timestamp = DateTime.Now;
    }
}
