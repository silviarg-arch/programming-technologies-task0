using System;
using System.Collections.Generic;
using System.Linq;
using ProgrammingTechnologiesTask2.Data.Database;
using ProgrammingTechnologiesTask2.Data.Models;

namespace ProgrammingTechnologiesTask2.Data.Repositories
{
    public class SqlCompactLibraryRepository : LibraryRepository
    {
        private readonly string connectionString;

        public SqlCompactLibraryRepository()
        {
            connectionString = DatabaseInitializer.EnsureDatabase();
        }

        public SqlCompactLibraryRepository(string databasePath)
        {
            DatabaseInitializer.EnsureDatabase(databasePath);
            connectionString = DatabaseInitializer.GetConnectionString(databasePath);
        }

        private LibraryDataContext CreateContext()
        {
            return new LibraryDataContext(connectionString);
        }

        public override IEnumerable<BookData> GetAllBooks()
        {
            using (LibraryDataContext context = CreateContext())
            {
                // LINQ query syntax
                var query =
                    from book in context.Books
                    orderby book.Title
                    select book;

                return query.Cast<BookData>().ToList();
            }
        }

        public override IEnumerable<BookData> SearchBooksByTitle(string titlePart)
        {
            if (titlePart == null)
            {
                titlePart = string.Empty;
            }

            using (LibraryDataContext context = CreateContext())
            {
                // LINQ query syntax
                var query =
                    from book in context.Books
                    where book.Title.Contains(titlePart)
                    orderby book.Title
                    select book;

                return query.Cast<BookData>().ToList();
            }
        }

        public override BookData GetBookById(int bookId)
        {
            using (LibraryDataContext context = CreateContext())
            {
                // LINQ method syntax
                return context.Books
                    .ToList()
                    .FirstOrDefault(book => book.BookId == bookId);
            }
        }

        public override int AddBook(string title, string author, int publicationYear)
        {
            using (LibraryDataContext context = CreateContext())
            {
                BookEntity book = new BookEntity
                {
                    Title = title,
                    Author = author,
                    PublicationYear = publicationYear,
                    IsAvailable = true
                };

                context.Books.InsertOnSubmit(book);
                context.SubmitChanges();

                return book.BookId;
            }
        }

        public override void UpdateBook(int bookId, string title, string author, int publicationYear)
        {
            using (LibraryDataContext context = CreateContext())
            {
                BookEntity book = context.Books
                    .ToList()
                    .FirstOrDefault(item => item.BookId == bookId);

                if (book == null)
                {
                    throw new InvalidOperationException("Book not found.");
                }

                book.Title = title;
                book.Author = author;
                book.PublicationYear = publicationYear;

                context.SubmitChanges();
            }
        }

        public override void DeleteBook(int bookId)
        {
            using (LibraryDataContext context = CreateContext())
            {
                BookEntity book = context.Books
                    .ToList()
                    .FirstOrDefault(item => item.BookId == bookId);

                if (book == null)
                {
                    throw new InvalidOperationException("Book not found.");
                }

                context.Books.DeleteOnSubmit(book);
                context.SubmitChanges();
            }
        }

        public override void SetBookAvailability(int bookId, bool isAvailable)
        {
            using (LibraryDataContext context = CreateContext())
            {
                BookEntity book = context.Books
                    .ToList()
                    .FirstOrDefault(item => item.BookId == bookId);

                if (book == null)
                {
                    throw new InvalidOperationException("Book not found.");
                }

                book.IsAvailable = isAvailable;

                context.SubmitChanges();
            }
        }

        public override IEnumerable<ReaderData> GetAllReaders()
        {
            using (LibraryDataContext context = CreateContext())
            {
                // LINQ method syntax
                return context.Readers
                    .OrderBy(reader => reader.Name)
                    .Cast<ReaderData>()
                    .ToList();
            }
        }

        public override ReaderData GetReaderById(int readerId)
        {
            using (LibraryDataContext context = CreateContext())
            {
                // LINQ method syntax
                return context.Readers
                    .ToList()
                    .FirstOrDefault(reader => reader.ReaderId == readerId);
            }
        }

        public override int AddReader(string name, string email)
        {
            using (LibraryDataContext context = CreateContext())
            {
                ReaderEntity reader = new ReaderEntity
                {
                    Name = name,
                    Email = email
                };

                context.Readers.InsertOnSubmit(reader);
                context.SubmitChanges();

                return reader.ReaderId;
            }
        }

        public override LoanData GetActiveLoanForBook(int bookId)
        {
            using (LibraryDataContext context = CreateContext())
            {
                // LINQ method syntax
                return context.Loans
                    .ToList()
                    .FirstOrDefault(loan => loan.BookId == bookId && loan.ReturnDate == null);
            }
        }

        public override int AddLoan(int bookId, int readerId, DateTime borrowDate)
        {
            using (LibraryDataContext context = CreateContext())
            {
                LoanEntity loan = new LoanEntity
                {
                    BookId = bookId,
                    ReaderId = readerId,
                    BorrowDate = borrowDate,
                    ReturnDate = null
                };

                context.Loans.InsertOnSubmit(loan);
                context.SubmitChanges();

                return loan.LoanId;
            }
        }

        public override void CloseLoan(int loanId, DateTime returnDate)
        {
            using (LibraryDataContext context = CreateContext())
            {
                LoanEntity loan = context.Loans
                    .ToList()
                    .FirstOrDefault(item => item.LoanId == loanId);

                if (loan == null)
                {
                    throw new InvalidOperationException("Loan not found.");
                }

                loan.ReturnDate = returnDate;

                context.SubmitChanges();
            }
        }

        public override void AddEvent(string eventType, int bookId, int? readerId, DateTime eventDate)
        {
            using (LibraryDataContext context = CreateContext())
            {
                LibraryEventEntity libraryEvent = new LibraryEventEntity
                {
                    EventType = eventType,
                    BookId = bookId,
                    ReaderId = readerId,
                    EventDate = eventDate
                };

                context.LibraryEvents.InsertOnSubmit(libraryEvent);
                context.SubmitChanges();
            }
        }

        public override IEnumerable<LibraryEventData> GetEvents()
        {
            using (LibraryDataContext context = CreateContext())
            {
                
                var query =
                    from libraryEvent in context.LibraryEvents
                    orderby libraryEvent.EventDate descending
                    select libraryEvent;

                return query.Cast<LibraryEventData>().ToList();
            }
        }
    }
}