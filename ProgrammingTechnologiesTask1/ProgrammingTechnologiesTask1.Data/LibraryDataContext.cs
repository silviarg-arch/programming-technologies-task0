namespace ProgrammingTechnologiesTask1.Data;

public class LibraryDataContext : DataContext
{
    public override Dictionary<string, User> Users { get; } = new();
    public override Dictionary<string, CatalogItem> Catalog { get; } = new();
    public override ProcessState State { get; } = new LibraryState();
    public override List<LibraryEvent> Events { get; } = new();
}