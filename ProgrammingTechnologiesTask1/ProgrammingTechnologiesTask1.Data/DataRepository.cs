namespace ProgrammingTechnologiesTask1.Data;

public abstract class DataRepository
{
    public abstract DataContext Context { get; }

    public abstract void AddUser(User user);
    public abstract void AddCatalogItem(CatalogItem item);
    public abstract void AddEvent(LibraryEvent libraryEvent);
}