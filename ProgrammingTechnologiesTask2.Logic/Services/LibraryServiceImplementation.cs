using System;
using System.Collections.Generic;
using System.Linq;
using ProgrammingTechnologiesTask2.Data.Models;
using ProgrammingTechnologiesTask2.Data.Repositories;
using ProgrammingTechnologiesTask2.Logic.Models;

namespace ProgrammingTechnologiesTask2.Logic.Services
{
    public class LibraryServiceImplementation : LibraryService
    {
        private readonly LibraryRepository repository;

        public LibraryServiceImplementation(LibraryRepository repository)
        {
            this.repository = repository;
        }

        public override IEnumerable<BookDto> GetBooks()
        {
            return repository.GetAllBooks()
                .Select(MapBook)
                .ToList();
        }

        public override IEnumerable<BookDto> SearchBooks(string titlePart)
        {
            return repository.SearchBooksByTitle(titlePart)
                .Select(MapBook)
                .ToList();
        }

        public override BookDto GetBookById(int bookId)
        {
            BookData book = repository.GetBookById(bookId);

            if (book == null)
            {
                return null;
            }

            return MapBook(book);
        }

        public override int AddBook(string title, string author, int publicationYear)
        {
            ValidateBookData(title, author, publicationYear);

            int bookId = repository.AddBook(title, author, publicationYear);
            repository.AddEvent("BookAdded", bookId, null, DateTime.Now);

            return bookId;
        }

        public override void UpdateBook(int bookId, string title, string author, int publicationYear)
        {
            ValidateBookData(title, author, publicationYear);

            BookData existingBook = repository.GetBookById(bookId);

            if (existingBook == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }

            repository.UpdateBook(bookId, title, author, publicationYear);
            repository.AddEvent("BookUpdated", bookId, null, DateTime.Now);
        }

        public override void DeleteBook(int bookId)
        {
            BookData existingBook = repository.GetBookById(bookId);

            if (existingBook == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }

            LoanData activeLoan = repository.GetActiveLoanForBook(bookId);

            if (activeLoan != null)
            {
                throw new InvalidOperationException("Cannot delete a borrowed book.");
            }

            repository.AddEvent("BookDeleted", bookId, null, DateTime.Now);
            repository.DeleteBook(bookId);
        }

        public override IEnumerable<ReaderDto> GetReaders()
        {
            return repository.GetAllReaders()
                .Select(MapReader)
                .ToList();
        }

        public override int AddReader(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Reader name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Reader email cannot be empty.");
            }

            return repository.AddReader(name, email);
        }

        public override void BorrowBook(int bookId, int readerId)
        {
            BookData book = repository.GetBookById(bookId);

            if (book == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }

            ReaderData reader = repository.GetReaderById(readerId);

            if (reader == null)
            {
                throw new InvalidOperationException("Reader does not exist.");
            }

            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book is already borrowed.");
            }

            LoanData activeLoan = repository.GetActiveLoanForBook(bookId);

            if (activeLoan != null)
            {
                throw new InvalidOperationException("Book already has an active loan.");
            }

            repository.AddLoan(bookId, readerId, DateTime.Now);
            repository.SetBookAvailability(bookId, false);
            repository.AddEvent("BookBorrowed", bookId, readerId, DateTime.Now);
        }

        public override void ReturnBook(int bookId)
        {
            BookData book = repository.GetBookById(bookId);

            if (book == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }

            LoanData activeLoan = repository.GetActiveLoanForBook(bookId);

            if (activeLoan == null)
            {
                throw new InvalidOperationException("Book is not currently borrowed.");
            }

            repository.CloseLoan(activeLoan.LoanId, DateTime.Now);
            repository.SetBookAvailability(bookId, true);
            repository.AddEvent("BookReturned", bookId, activeLoan.ReaderId, DateTime.Now);
        }

        public override IEnumerable<LibraryEventDto> GetEvents()
        {
            return repository.GetEvents()
                .Select(MapEvent)
                .ToList();
        }

        private static void ValidateBookData(string title, string author, int publicationYear)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Book title cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Book author cannot be empty.");
            }

            if (publicationYear <= 0)
            {
                throw new ArgumentException("Publication year must be positive.");
            }
        }

        private static BookDto MapBook(BookData book)
        {
            return new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable
            };
        }

        private static ReaderDto MapReader(ReaderData reader)
        {
            return new ReaderDto
            {
                ReaderId = reader.ReaderId,
                Name = reader.Name,
                Email = reader.Email
            };
        }

        private static LibraryEventDto MapEvent(LibraryEventData libraryEvent)
        {
            return new LibraryEventDto
            {
                EventId = libraryEvent.EventId,
                EventType = libraryEvent.EventType,
                BookId = libraryEvent.BookId,
                ReaderId = libraryEvent.ReaderId,
                EventDate = libraryEvent.EventDate
            };
        }
    }
}