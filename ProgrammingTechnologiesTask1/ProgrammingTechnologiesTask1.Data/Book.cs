namespace ProgrammingTechnologiesTask1.Data;

public class Book
{
    public string BookId { get; }
    public string Title { get; }
    public string Author { get; }

    public Book(string bookId, string title, string author)
    {
        BookId = bookId;
        Title = title;
        Author = author;
    }
}