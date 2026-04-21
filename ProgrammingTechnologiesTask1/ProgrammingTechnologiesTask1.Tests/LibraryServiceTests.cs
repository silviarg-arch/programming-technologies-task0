using ProgrammingTechnologiesTask1.Data;
using ProgrammingTechnologiesTask1.Logic;

namespace ProgrammingTechnologiesTask1.Tests;

public class LibraryServiceTests
{
    [Fact]
    public void BorrowBook_WhenBookIsAvailable_ShouldBorrowSuccessfully()
    {
        IDataRepository repository = TestDataGenerator.CreateRepositoryWithSampleData();
        ILibraryService service = new LibraryService(repository);

        service.BorrowBook("R1", "B1");

        Assert.False(service.IsBookAvailable("B1"));
        Assert.Equal("R1", repository.Context.State.BorrowedBooks["B1"]);
        Assert.Equal(1, service.GetEventCount());
    }

    [Fact]
    public void BorrowBook_WhenBookIsAlreadyBorrowed_ShouldThrowException()
    {
        IDataRepository repository = TestDataGenerator.CreateRepositoryWithSampleData();
        ILibraryService service = new LibraryService(repository);

        service.BorrowBook("R1", "B1");

        Assert.Throws<InvalidOperationException>(() => service.BorrowBook("R2", "B1"));
    }

    [Fact]
    public void ReturnBook_WhenBookIsBorrowed_ShouldReturnSuccessfully()
    {
        IDataRepository repository = TestDataGenerator.CreateRepositoryWithSampleData();
        ILibraryService service = new LibraryService(repository);

        service.BorrowBook("R1", "B1");
        service.ReturnBook("R1", "B1");

        Assert.True(service.IsBookAvailable("B1"));
        Assert.Empty(repository.Context.State.BorrowedBooks);
        Assert.Equal(2, service.GetEventCount());
    }

    [Fact]
    public void ReturnBook_WhenBookIsNotBorrowed_ShouldThrowException()
    {
        IDataRepository repository = TestDataGenerator.CreateRepositoryWithSampleData();
        ILibraryService service = new LibraryService(repository);

        Assert.Throws<InvalidOperationException>(() => service.ReturnBook("R1", "B1"));
    }

    [Fact]
    public void IsBookAvailable_WhenBookHasNotBeenBorrowed_ShouldReturnTrue()
    {
        IDataRepository repository = TestDataGenerator.CreateRepositoryWithSampleData();
        ILibraryService service = new LibraryService(repository);

        bool result = service.IsBookAvailable("B1");

        Assert.True(result);
    }

    [Fact]
    public void RegisterReader_ShouldAddNewReader()
    {
        IDataRepository repository = TestDataGenerator.CreateEmptyRepository();
        ILibraryService service = new LibraryService(repository);

        service.RegisterReader("R10", "Charlie");

        Assert.True(repository.Context.Users.ContainsKey("R10"));
        Assert.Equal("Charlie", repository.Context.Users["R10"].Name);
    }

    [Fact]
    public void AddBook_ShouldAddNewBookToCatalog()
    {
        IDataRepository repository = TestDataGenerator.CreateEmptyRepository();
        ILibraryService service = new LibraryService(repository);

        service.AddBook("B10", "The Hobbit", "J.R.R. Tolkien");

        Assert.True(repository.Context.Catalog.ContainsKey("B10"));
        Assert.Equal("The Hobbit", repository.Context.Catalog["B10"].Title);
    }
}