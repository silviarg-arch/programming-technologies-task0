using ProgrammingTechnologiesTask1.Data;

namespace ProgrammingTechnologiesTask1.Logic;

public class LibraryService : ILibraryService
{
    private readonly DataRepository repository;

    public LibraryService(DataRepository repository)
    {
        this.repository = repository;
    }

    public void RegisterReader(string userId, string name)
    {
        if (repository.Context.Users.ContainsKey(userId))
        {
            throw new InvalidOperationException("Reader already exists.");
        }

        User reader = new Reader(userId, name);
        repository.AddUser(reader);
    }

    public void AddBook(string itemId, string title, string author)
    {
        if (repository.Context.Catalog.ContainsKey(itemId))
        {
            throw new InvalidOperationException("Book already exists.");
        }

        CatalogItem book = new Book(itemId, title, author);
        repository.AddCatalogItem(book);
    }

    public void BorrowBook(string userId, string itemId)
    {
        if (!repository.Context.Users.ContainsKey(userId))
        {
            throw new InvalidOperationException("Reader does not exist.");
        }

        if (!repository.Context.Catalog.ContainsKey(itemId))
        {
            throw new InvalidOperationException("Book does not exist.");
        }

        LibraryState state = GetLibraryState();

        if (state.BorrowedBooks.ContainsKey(itemId))
        {
            throw new InvalidOperationException("Book is already borrowed.");
        }

        state.BorrowedBooks[itemId] = userId;

        LibraryEvent borrowEvent = new BorrowEvent(DateTime.Now, userId, itemId);
        repository.AddEvent(borrowEvent);
    }

    public void ReturnBook(string userId, string itemId)
    {
        LibraryState state = GetLibraryState();

        if (!state.BorrowedBooks.ContainsKey(itemId))
        {
            throw new InvalidOperationException("Book is not borrowed.");
        }

        string currentUserId = state.BorrowedBooks[itemId];

        if (currentUserId != userId)
        {
            throw new InvalidOperationException("This reader did not borrow the book.");
        }

        state.BorrowedBooks.Remove(itemId);

        LibraryEvent returnEvent = new ReturnEvent(DateTime.Now, userId, itemId);
        repository.AddEvent(returnEvent);
    }

    public bool IsBookAvailable(string itemId)
    {
        if (!repository.Context.Catalog.ContainsKey(itemId))
        {
            throw new InvalidOperationException("Book does not exist.");
        }

        LibraryState state = GetLibraryState();

        return !state.BorrowedBooks.ContainsKey(itemId);
    }

    public int GetEventCount()
    {
        return repository.Context.Events.Count;
    }

    private LibraryState GetLibraryState()
    {
        if (repository.Context.State is not LibraryState state)
        {
            throw new InvalidOperationException("Invalid library state.");
        }

        return state;
    }
}