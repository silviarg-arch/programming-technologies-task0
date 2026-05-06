using ProgrammingTechnologiesTask1.Data;

namespace ProgrammingTechnologiesTask1.Tests;

public static class TestDataGenerator
{
    public static DataRepository CreateEmptyRepository()
    {
        DataContext context = new LibraryDataContext();
        return new LibraryDataRepository(context);
    }

    public static DataRepository CreateRepositoryWithSampleData()
    {
        DataContext context = new LibraryDataContext();
        DataRepository repository = new LibraryDataRepository(context);

        repository.AddUser(new Reader("R1", "Alice"));
        repository.AddUser(new Reader("R2", "Bob"));

        repository.AddCatalogItem(new Book("B1", "1984", "George Orwell"));
        repository.AddCatalogItem(new Book("B2", "Dune", "Frank Herbert"));

        return repository;
    }
}