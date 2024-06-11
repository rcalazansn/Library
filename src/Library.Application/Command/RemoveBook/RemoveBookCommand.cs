using MediatR;

namespace Library.Application.Command.RemoveBook
{
    public class RemoveBookCommand : IRequest
    {
        public RemoveBookCommand(int id) => Id = id;

        public int Id { get; private set; }
    }
}
