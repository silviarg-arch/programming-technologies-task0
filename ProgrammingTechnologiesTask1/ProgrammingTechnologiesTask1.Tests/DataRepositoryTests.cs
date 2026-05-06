using ProgrammingTechnologiesTask1.Data;

namespace ProgrammingTechnologiesTask1.Tests;

public class DataRepositoryTests
{
    [Fact]
    public void AddUser_ShouldStoreReaderInUsersCollection()
    {
        DataRepository repository = TestDataGenerator.CreateEmptyRepository();
        Reader reader = new("R1", "Alice");

        repository.AddUser(reader);

        Assert.True(repository.Context.Users.ContainsKey("R1"));
        Assert.Equal("Alice", repository.Context.Users["R1"].Name);
    }

    [Fact]
    public void AddCatalogItem_ShouldStoreBookInCatalog()
    {
        DataRepository repository = TestDataGenerator.CreateEmptyRepository();
        Book book = new("B1", "1984", "George Orwell");

        repository.AddCatalogItem(book);

        Assert.True(repository.Context.Catalog.ContainsKey("B1"));
        Assert.Equal("1984", repository.Context.Catalog["B1"].Title);
    }

    [Fact]
    public void AddEvent_ShouldStoreEventInEventsCollection()
    {
        DataRepository repository = TestDataGenerator.CreateEmptyRepository();
        LibraryEvent libraryEvent = new BorrowEvent(DateTime.Now, "R1", "B1");

        repository.AddEvent(libraryEvent);

        Assert.Single(repository.Context.Events);
        Assert.IsType<BorrowEvent>(repository.Context.Events[0]);
    }
}