using ProgrammingTechnologiesTask1.Data;

namespace ProgrammingTechnologiesTask1.Logic;

public class LibraryService : ILibraryService
{
    private readonly IDataRepository repository;

    public LibraryService(IDataRepository repository)
    {
        this.repository = repository;
    }

    public void RegisterReader(string readerId, string name)
    {
        if (repository.Context.Users.ContainsKey(readerId))
        {
            throw new InvalidOperationException("Reader already exists.");
        }

        Reader reader = new(readerId, name);
        repository.AddReader(reader);
    }

    public void AddBook(string bookId, string title, string author)
    {
        if (repository.Context.Catalog.ContainsKey(bookId))
        {
            throw new InvalidOperationException("Book already exists.");
        }

        Book book = new(bookId, title, author);
        repository.AddBook(book);
    }

    public void BorrowBook(string readerId, string bookId)
    {
        if (!repository.Context.Users.ContainsKey(readerId))
        {
            throw new InvalidOperationException("Reader does not exist.");
        }

        if (!repository.Context.Catalog.ContainsKey(bookId))
        {
            throw new InvalidOperationException("Book does not exist.");
        }

        if (repository.Context.State.BorrowedBooks.ContainsKey(bookId))
        {
            throw new InvalidOperationException("Book is already borrowed.");
        }

        repository.Context.State.BorrowedBooks[bookId] = readerId;

        BorrowEvent borrowEvent = new(DateTime.Now, readerId, bookId);
        repository.AddEvent(borrowEvent);
    }

    public void ReturnBook(string readerId, string bookId)
    {
        if (!repository.Context.State.BorrowedBooks.ContainsKey(bookId))
        {
            throw new InvalidOperationException("Book is not borrowed.");
        }

        string currentReaderId = repository.Context.State.BorrowedBooks[bookId];

        if (currentReaderId != readerId)
        {
            throw new InvalidOperationException("This reader did not borrow the book.");
        }

        repository.Context.State.BorrowedBooks.Remove(bookId);

        ReturnEvent returnEvent = new(DateTime.Now, readerId, bookId);
        repository.AddEvent(returnEvent);
    }

    public bool IsBookAvailable(string bookId)
    {
        if (!repository.Context.Catalog.ContainsKey(bookId))
        {
            throw new InvalidOperationException("Book does not exist.");
        }

        return !repository.Context.State.BorrowedBooks.ContainsKey(bookId);
    }

    public int GetEventCount()
    {
        return repository.Context.Events.Count;
    }
}