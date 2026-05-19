using System;
using System.Collections.Generic;
using System.Linq;
using ProgrammingTechnologiesTask2.Data.Models;
using ProgrammingTechnologiesTask2.Data.Repositories;

namespace ProgrammingTechnologiesTask2.Tests.TestDoubles
{
    public class FakeLibraryRepository : LibraryRepository
    {
        private readonly List<FakeBookData> books = new List<FakeBookData>();
        private readonly List<FakeReaderData> readers = new List<FakeReaderData>();
        private readonly List<FakeLoanData> loans = new List<FakeLoanData>();
        private readonly List<FakeLibraryEventData> events = new List<FakeLibraryEventData>();

        private int nextBookId = 1;
        private int nextReaderId = 1;
        private int nextLoanId = 1;
        private int nextEventId = 1;

        public override IEnumerable<BookData> GetAllBooks()
        {
            return books.OrderBy(book => book.Title).Cast<BookData>().ToList();
        }

        public override IEnumerable<BookData> SearchBooksByTitle(string titlePart)
        {
            if (titlePart == null)
            {
                titlePart = string.Empty;
            }

            return books
                .Where(book => book.Title.Contains(titlePart))
                .OrderBy(book => book.Title)
                .Cast<BookData>()
                .ToList();
        }

        public override BookData GetBookById(int bookId)
        {
            return books.FirstOrDefault(book => book.BookId == bookId);
        }

        public override int AddBook(string title, string author, int publicationYear)
        {
            FakeBookData book = new FakeBookData
            {
                BookId = nextBookId++,
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                IsAvailable = true
            };

            books.Add(book);
            return book.BookId;
        }

        public override void UpdateBook(int bookId, string title, string author, int publicationYear)
        {
            FakeBookData book = books.FirstOrDefault(item => item.BookId == bookId);

            if (book == null)
            {
                throw new InvalidOperationException("Book not found.");
            }

            book.Title = title;
            book.Author = author;
            book.PublicationYear = publicationYear;
        }

        public override void DeleteBook(int bookId)
        {
            FakeBookData book = books.FirstOrDefault(item => item.BookId == bookId);

            if (book == null)
            {
                throw new InvalidOperationException("Book not found.");
            }

            books.Remove(book);
        }

        public override void SetBookAvailability(int bookId, bool isAvailable)
        {
            FakeBookData book = books.FirstOrDefault(item => item.BookId == bookId);

            if (book == null)
            {
                throw new InvalidOperationException("Book not found.");
            }

            book.IsAvailable = isAvailable;
        }

        public override IEnumerable<ReaderData> GetAllReaders()
        {
            return readers.OrderBy(reader => reader.Name).Cast<ReaderData>().ToList();
        }

        public override ReaderData GetReaderById(int readerId)
        {
            return readers.FirstOrDefault(reader => reader.ReaderId == readerId);
        }

        public override int AddReader(string name, string email)
        {
            FakeReaderData reader = new FakeReaderData
            {
                ReaderId = nextReaderId++,
                Name = name,
                Email = email
            };

            readers.Add(reader);
            return reader.ReaderId;
        }

        public override LoanData GetActiveLoanForBook(int bookId)
        {
            return loans.FirstOrDefault(loan => loan.BookId == bookId && loan.ReturnDate == null);
        }

        public override int AddLoan(int bookId, int readerId, DateTime borrowDate)
        {
            FakeLoanData loan = new FakeLoanData
            {
                LoanId = nextLoanId++,
                BookId = bookId,
                ReaderId = readerId,
                BorrowDate = borrowDate,
                ReturnDate = null
            };

            loans.Add(loan);
            return loan.LoanId;
        }

        public override void CloseLoan(int loanId, DateTime returnDate)
        {
            FakeLoanData loan = loans.FirstOrDefault(item => item.LoanId == loanId);

            if (loan == null)
            {
                throw new InvalidOperationException("Loan not found.");
            }

            loan.ReturnDate = returnDate;
        }

        public override void AddEvent(string eventType, int bookId, int? readerId, DateTime eventDate)
        {
            FakeLibraryEventData libraryEvent = new FakeLibraryEventData
            {
                EventId = nextEventId++,
                EventType = eventType,
                BookId = bookId,
                ReaderId = readerId,
                EventDate = eventDate
            };

            events.Add(libraryEvent);
        }

        public override IEnumerable<LibraryEventData> GetEvents()
        {
            return events.OrderByDescending(item => item.EventDate).Cast<LibraryEventData>().ToList();
        }
    }

    public class FakeBookData : BookData
    {
        public override int BookId { get; set; }
        public override string Title { get; set; }
        public override string Author { get; set; }
        public override int PublicationYear { get; set; }
        public override bool IsAvailable { get; set; }
    }

    public class FakeReaderData : ReaderData
    {
        public override int ReaderId { get; set; }
        public override string Name { get; set; }
        public override string Email { get; set; }
    }

    public class FakeLoanData : LoanData
    {
        public override int LoanId { get; set; }
        public override int BookId { get; set; }
        public override int ReaderId { get; set; }
        public override DateTime BorrowDate { get; set; }
        public override DateTime? ReturnDate { get; set; }
    }

    public class FakeLibraryEventData : LibraryEventData
    {
        public override int EventId { get; set; }
        public override string EventType { get; set; }
        public override int BookId { get; set; }
        public override int? ReaderId { get; set; }
        public override DateTime EventDate { get; set; }
    }
}