using ProgrammingTechnologiesTask1.Data;

namespace ProgrammingTechnologiesTask1.Tests;

public static class TestDataGenerator
{
    public static IDataRepository CreateEmptyRepository()
    {
        IDataContext context = new DataContext();
        return new DataRepository(context);
    }

    public static IDataRepository CreateRepositoryWithSampleData()
    {
        IDataContext context = new DataContext();
        IDataRepository repository = new DataRepository(context);

        repository.AddReader(new Reader("R1", "Alice"));
        repository.AddReader(new Reader("R2", "Bob"));

        repository.AddBook(new Book("B1", "1984", "George Orwell"));
        repository.AddBook(new Book("B2", "Dune", "Frank Herbert"));

        return repository;
    }
}