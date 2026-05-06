namespace ProgrammingTechnologiesTask1.Data;

public class Book : CatalogItem
{
    public override string ItemId { get; }
    public override string Title { get; }
    public string Author { get; }

    public Book(string itemId, string title, string author)
    {
        ItemId = itemId;
        Title = title;
        Author = author;
    }
}