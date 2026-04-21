namespace ProgrammingTechnologiesTask1.Data;

public class DataRepository : IDataRepository
{
    public IDataContext Context { get; }

    public DataRepository(IDataContext context)
    {
        Context = context;
    }

    public void AddReader(Reader reader)
    {
        Context.Users[reader.ReaderId] = reader;
    }

    public void AddBook(Book book)
    {
        Context.Catalog[book.BookId] = book;
    }

    public void AddEvent(LibraryEvent libraryEvent)
    {
        Context.Events.Add(libraryEvent);
    }
}