namespace ProgrammingTechnologiesTask1.Data;

public class LibraryDataRepository : DataRepository
{
    public override DataContext Context { get; }

    public LibraryDataRepository(DataContext context)
    {
        Context = context;
    }

    public override void AddUser(User user)
    {
        Context.Users[user.UserId] = user;
    }

    public override void AddCatalogItem(CatalogItem item)
    {
        Context.Catalog[item.ItemId] = item;
    }

    public override void AddEvent(LibraryEvent libraryEvent)
    {
        Context.Events.Add(libraryEvent);
    }
}