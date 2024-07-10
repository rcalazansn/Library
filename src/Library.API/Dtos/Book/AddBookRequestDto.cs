namespace Library.API.Dtos.Book
{
    public class AddBookRequestDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int YearOfPublication { get; set; }
    }
}
