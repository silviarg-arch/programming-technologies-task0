using System;
using System.IO;
using ProgrammingTechnologiesTask2.Data.Repositories;
using ProgrammingTechnologiesTask2.Tests.TestDoubles;

namespace ProgrammingTechnologiesTask2.Tests.TestData
{
    public static class TestDataGenerator
    {
        public static string CreateTemporaryDatabasePath()
        {
            string fileName = "LibraryTest_" + Guid.NewGuid().ToString("N") + ".sdf";
            return Path.Combine(Path.GetTempPath(), fileName);
        }

        public static LibraryRepository CreateEmptySqlRepository()
        {
            string databasePath = CreateTemporaryDatabasePath();
            return new SqlCompactLibraryRepository(databasePath);
        }

        public static FakeLibraryRepository CreateEmptyFakeRepository()
        {
            return new FakeLibraryRepository();
        }

        public static FakeLibraryRepository CreateFakeRepositoryWithSampleData()
        {
            FakeLibraryRepository repository = new FakeLibraryRepository();

            repository.AddBook("1984", "George Orwell", 1949);
            repository.AddBook("Dune", "Frank Herbert", 1965);

            repository.AddReader("Alice Johnson", "alice@example.com");
            repository.AddReader("Bob Smith", "bob@example.com");

            return repository;
        }
    }
}