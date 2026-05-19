using System.Collections.Generic;
using ProgrammingTechnologiesTask2.Logic.Models;

namespace ProgrammingTechnologiesTask2.Logic.Services
{
    public abstract class LibraryService
    {
        public abstract IEnumerable<BookDto> GetBooks();
        public abstract IEnumerable<BookDto> SearchBooks(string titlePart);
        public abstract BookDto GetBookById(int bookId);

        public abstract int AddBook(string title, string author, int publicationYear);
        public abstract void UpdateBook(int bookId, string title, string author, int publicationYear);
        public abstract void DeleteBook(int bookId);

        public abstract IEnumerable<ReaderDto> GetReaders();
        public abstract int AddReader(string name, string email);

        public abstract void BorrowBook(int bookId, int readerId);
        public abstract void ReturnBook(int bookId);

        public abstract IEnumerable<LibraryEventDto> GetEvents();
    }
}