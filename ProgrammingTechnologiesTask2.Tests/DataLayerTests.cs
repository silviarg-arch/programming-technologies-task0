using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologiesTask2.Data.Models;
using ProgrammingTechnologiesTask2.Data.Repositories;
using ProgrammingTechnologiesTask2.Tests.TestData;

namespace ProgrammingTechnologiesTask2.Tests
{
    [TestClass]
    public class DataLayerTests
    {
        [TestMethod]
        public void AddBook_ShouldPersistBookInSqlDatabase()
        {
            LibraryRepository repository = TestDataGenerator.CreateEmptySqlRepository();

            int bookId = repository.AddBook("Test Book", "Test Author", 2024);

            BookData book = repository.GetBookById(bookId);

            Assert.IsNotNull(book);
            Assert.AreEqual("Test Book", book.Title);
            Assert.AreEqual("Test Author", book.Author);
            Assert.AreEqual(2024, book.PublicationYear);
            Assert.IsTrue(book.IsAvailable);
        }

        [TestMethod]
        public void SearchBooksByTitle_ShouldReturnMatchingBooks()
        {
            LibraryRepository repository = TestDataGenerator.CreateEmptySqlRepository();

            repository.AddBook("Clean Code", "Robert C. Martin", 2008);
            repository.AddBook("Dune", "Frank Herbert", 1965);

            BookData result = repository.SearchBooksByTitle("Clean").FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual("Clean Code", result.Title);
        }

        [TestMethod]
        public void AddReader_ShouldPersistReaderInSqlDatabase()
        {
            LibraryRepository repository = TestDataGenerator.CreateEmptySqlRepository();

            int readerId = repository.AddReader("Test Reader", "reader@test.com");

            ReaderData reader = repository.GetReaderById(readerId);

            Assert.IsNotNull(reader);
            Assert.AreEqual("Test Reader", reader.Name);
            Assert.AreEqual("reader@test.com", reader.Email);
        }
    }
}