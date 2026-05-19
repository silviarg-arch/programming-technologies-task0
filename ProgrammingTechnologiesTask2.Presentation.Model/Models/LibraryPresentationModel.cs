using System;
using System.Collections.Generic;
using System.Linq;
using ProgrammingTechnologiesTask2.Logic.Models;
using ProgrammingTechnologiesTask2.Logic.Services;

namespace ProgrammingTechnologiesTask2.Presentation.Model.Models
{
    public delegate void LibraryModelChangedEventHandler(object sender, EventArgs e);

    public class LibraryPresentationModel
    {
        private readonly LibraryService libraryService;

        public event LibraryModelChangedEventHandler DataChanged;

        public LibraryPresentationModel(LibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public IEnumerable<BookModel> GetBooks()
        {
            return libraryService.GetBooks()
                .Select(MapBook)
                .ToList();
        }

        public IEnumerable<BookModel> SearchBooks(string titlePart)
        {
            return libraryService.SearchBooks(titlePart)
                .Select(MapBook)
                .ToList();
        }

        public IEnumerable<ReaderModel> GetReaders()
        {
            return libraryService.GetReaders()
                .Select(MapReader)
                .ToList();
        }

        public IEnumerable<LibraryEventModel> GetEvents()
        {
            return libraryService.GetEvents()
                .Select(MapEvent)
                .ToList();
        }

        public void AddBook(string title, string author, int publicationYear)
        {
            libraryService.AddBook(title, author, publicationYear);
            OnDataChanged();
        }

        public void UpdateBook(int bookId, string title, string author, int publicationYear)
        {
            libraryService.UpdateBook(bookId, title, author, publicationYear);
            OnDataChanged();
        }

        public void DeleteBook(int bookId)
        {
            libraryService.DeleteBook(bookId);
            OnDataChanged();
        }

        public void AddReader(string name, string email)
        {
            libraryService.AddReader(name, email);
            OnDataChanged();
        }

        public void BorrowBook(int bookId, int readerId)
        {
            libraryService.BorrowBook(bookId, readerId);
            OnDataChanged();
        }

        public void ReturnBook(int bookId)
        {
            libraryService.ReturnBook(bookId);
            OnDataChanged();
        }

        private void OnDataChanged()
        {
            LibraryModelChangedEventHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private static BookModel MapBook(BookDto book)
        {
            return new BookModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable
            };
        }

        private static ReaderModel MapReader(ReaderDto reader)
        {
            return new ReaderModel
            {
                ReaderId = reader.ReaderId,
                Name = reader.Name,
                Email = reader.Email
            };
        }

        private static LibraryEventModel MapEvent(LibraryEventDto libraryEvent)
        {
            return new LibraryEventModel
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