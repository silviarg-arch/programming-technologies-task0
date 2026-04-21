namespace ProgrammingTechnologiesTask1.Data;

public interface IDataRepository
{
    IDataContext Context { get; }

    void AddReader(Reader reader);
    void AddBook(Book book);
    void AddEvent(LibraryEvent libraryEvent);
}