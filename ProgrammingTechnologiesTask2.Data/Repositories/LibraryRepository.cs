using System;
using System.Collections.Generic;
using ProgrammingTechnologiesTask2.Data.Models;

namespace ProgrammingTechnologiesTask2.Data.Repositories
{
    public abstract class LibraryRepository
    {
        public abstract IEnumerable<BookData> GetAllBooks();
        public abstract IEnumerable<BookData> SearchBooksByTitle(string titlePart);
        public abstract BookData GetBookById(int bookId);

        public abstract int AddBook(string title, string author, int publicationYear);
        public abstract void UpdateBook(int bookId, string title, string author, int publicationYear);
        public abstract void DeleteBook(int bookId);
        public abstract void SetBookAvailability(int bookId, bool isAvailable);

        public abstract IEnumerable<ReaderData> GetAllReaders();
        public abstract ReaderData GetReaderById(int readerId);
        public abstract int AddReader(string name, string email);

        public abstract LoanData GetActiveLoanForBook(int bookId);
        public abstract int AddLoan(int bookId, int readerId, DateTime borrowDate);
        public abstract void CloseLoan(int loanId, DateTime returnDate);

        public abstract void AddEvent(string eventType, int bookId, int? readerId, DateTime eventDate);
        public abstract IEnumerable<LibraryEventData> GetEvents();
    }
}