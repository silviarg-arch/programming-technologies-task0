namespace ProgrammingTechnologiesTask1.Data;

public interface IDataContext
{
    Dictionary<string, Book> Catalog { get; }
    Dictionary<string, Reader> Users { get; }
    LibraryState State { get; }
    List<LibraryEvent> Events { get; }
}