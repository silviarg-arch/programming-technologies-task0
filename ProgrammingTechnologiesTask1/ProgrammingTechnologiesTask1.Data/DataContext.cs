namespace ProgrammingTechnologiesTask1.Data;

public class DataContext : IDataContext
{
    public Dictionary<string, Book> Catalog { get; } = new();
    public Dictionary<string, Reader> Users { get; } = new();
    public LibraryState State { get; } = new();
    public List<LibraryEvent> Events { get; } = new();
}