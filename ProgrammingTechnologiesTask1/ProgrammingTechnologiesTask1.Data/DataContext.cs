namespace ProgrammingTechnologiesTask1.Data;

public abstract class DataContext
{
    public abstract Dictionary<string, User> Users { get; }
    public abstract Dictionary<string, CatalogItem> Catalog { get; }
    public abstract ProcessState State { get; }
    public abstract List<LibraryEvent> Events { get; }
}