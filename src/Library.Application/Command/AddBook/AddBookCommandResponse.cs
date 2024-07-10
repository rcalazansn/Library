namespace Library.Application.Command.AddBook
{
    public class AddBookCommandResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int YearOfPublication { get; set; }
    }
}
