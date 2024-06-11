using MediatR;

namespace Library.Application.Command.ReturnBook
{
    public class ReturnBookCommand : IRequest
    {
        public ReturnBookCommand(int id) => Id = id;

        public int Id { get; private set; }
    }
}
