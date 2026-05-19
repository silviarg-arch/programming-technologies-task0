using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologiesTask2.Data.Repositories;
using ProgrammingTechnologiesTask2.Logic.Models;
using ProgrammingTechnologiesTask2.Logic.Services;
using ProgrammingTechnologiesTask2.Tests.TestData;

namespace ProgrammingTechnologiesTask2.Tests
{
    [TestClass]
    public class LogicLayerTests
    {
        [TestMethod]
        public void BorrowBook_WhenBookIsAvailable_ShouldBorrowSuccessfully()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);

            service.BorrowBook(1, 1);

            BookDto book = service.GetBookById(1);

            Assert.IsFalse(book.IsAvailable);
            Assert.AreEqual(1, service.GetEvents().Count(item => item.EventType == "BookBorrowed"));
        }

        [TestMethod]
        public void BorrowBook_WhenBookIsAlreadyBorrowed_ShouldThrowException()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);

            service.BorrowBook(1, 1);

            Assert.ThrowsException<InvalidOperationException>(() => service.BorrowBook(1, 2));
        }

        [TestMethod]
        public void ReturnBook_WhenBookIsBorrowed_ShouldReturnSuccessfully()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);

            service.BorrowBook(1, 1);
            service.ReturnBook(1);

            BookDto book = service.GetBookById(1);

            Assert.IsTrue(book.IsAvailable);
            Assert.AreEqual(1, service.GetEvents().Count(item => item.EventType == "BookReturned"));
        }

        [TestMethod]
        public void AddBook_WhenTitleIsEmpty_ShouldThrowException()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);

            Assert.ThrowsException<ArgumentException>(() => service.AddBook("", "Author", 2020));
        }
    }
}