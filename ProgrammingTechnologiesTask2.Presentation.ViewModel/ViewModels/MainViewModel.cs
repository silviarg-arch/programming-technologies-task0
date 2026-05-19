using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProgrammingTechnologiesTask2.Presentation.Model.Models;
using ProgrammingTechnologiesTask2.Presentation.ViewModel.Commands;

namespace ProgrammingTechnologiesTask2.Presentation.ViewModel.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly LibraryPresentationModel model;

        private BookModel selectedBook;
        private ReaderModel selectedReader;
        private string searchText;
        private string newTitle;
        private string newAuthor;
        private string newPublicationYear;
        private string statusMessage;

        public MainViewModel(LibraryPresentationModel model)
        {
            this.model = model;

            Books = new ObservableCollection<BookModel>();
            Readers = new ObservableCollection<ReaderModel>();
            Events = new ObservableCollection<LibraryEventModel>();

            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            SearchCommand = new AsyncRelayCommand(SearchAsync);
            AddBookCommand = new AsyncRelayCommand(AddBookAsync, CanAddBook);
            UpdateBookCommand = new AsyncRelayCommand(UpdateBookAsync, CanEditSelectedBook);
            DeleteBookCommand = new AsyncRelayCommand(DeleteBookAsync, CanEditSelectedBook);
            BorrowBookCommand = new AsyncRelayCommand(BorrowBookAsync, CanBorrowBook);
            ReturnBookCommand = new AsyncRelayCommand(ReturnBookAsync, CanReturnBook);
            ClearEditorCommand = new RelayCommand(ClearEditor);

            this.model.DataChanged += OnModelDataChanged;
        }

        public ObservableCollection<BookModel> Books { get; private set; }

        public ObservableCollection<ReaderModel> Readers { get; private set; }

        public ObservableCollection<LibraryEventModel> Events { get; private set; }

        public BookModel SelectedBook
        {
            get
            {
                return selectedBook;
            }
            set
            {
                selectedBook = value;
                OnPropertyChanged();

                CopySelectedBookToEditor();
                RaiseCommandStates();
            }
        }

        public ReaderModel SelectedReader
        {
            get
            {
                return selectedReader;
            }
            set
            {
                selectedReader = value;
                OnPropertyChanged();
                RaiseCommandStates();
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public string NewTitle
        {
            get
            {
                return newTitle;
            }
            set
            {
                newTitle = value;
                OnPropertyChanged();
                RaiseCommandStates();
            }
        }

        public string NewAuthor
        {
            get
            {
                return newAuthor;
            }
            set
            {
                newAuthor = value;
                OnPropertyChanged();
                RaiseCommandStates();
            }
        }

        public string NewPublicationYear
        {
            get
            {
                return newPublicationYear;
            }
            set
            {
                newPublicationYear = value;
                OnPropertyChanged();
                RaiseCommandStates();
            }
        }

        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                statusMessage = value;
                OnPropertyChanged();
            }
        }

        public AsyncRelayCommand LoadDataCommand { get; private set; }

        public AsyncRelayCommand SearchCommand { get; private set; }

        public AsyncRelayCommand AddBookCommand { get; private set; }

        public AsyncRelayCommand UpdateBookCommand { get; private set; }

        public AsyncRelayCommand DeleteBookCommand { get; private set; }

        public AsyncRelayCommand BorrowBookCommand { get; private set; }

        public AsyncRelayCommand ReturnBookCommand { get; private set; }

        public RelayCommand ClearEditorCommand { get; private set; }

        private async void OnModelDataChanged(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                List<BookModel> books = await Task.Run(() => model.GetBooks().ToList());
                List<ReaderModel> readers = await Task.Run(() => model.GetReaders().ToList());
                List<LibraryEventModel> events = await Task.Run(() => model.GetEvents().ToList());

                ReplaceCollection(Books, books);
                ReplaceCollection(Readers, readers);
                ReplaceCollection(Events, events);

                StatusMessage = "Data loaded successfully.";
                RaiseCommandStates();
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private async Task SearchAsync()
        {
            try
            {
                List<BookModel> books = await Task.Run(() => model.SearchBooks(SearchText).ToList());

                ReplaceCollection(Books, books);

                StatusMessage = "Search completed.";
                RaiseCommandStates();
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private async Task AddBookAsync()
        {
            try
            {
                int publicationYear;

                if (!int.TryParse(NewPublicationYear, out publicationYear))
                {
                    StatusMessage = "Publication year must be a number.";
                    return;
                }

                await Task.Run(() => model.AddBook(NewTitle, NewAuthor, publicationYear));

                ClearEditor();
                StatusMessage = "Book added successfully.";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private async Task UpdateBookAsync()
        {
            try
            {
                if (SelectedBook == null)
                {
                    StatusMessage = "No book selected.";
                    return;
                }

                int publicationYear;

                if (!int.TryParse(NewPublicationYear, out publicationYear))
                {
                    StatusMessage = "Publication year must be a number.";
                    return;
                }

                int bookId = SelectedBook.BookId;

                await Task.Run(() => model.UpdateBook(bookId, NewTitle, NewAuthor, publicationYear));

                StatusMessage = "Book updated successfully.";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private async Task DeleteBookAsync()
        {
            try
            {
                if (SelectedBook == null)
                {
                    StatusMessage = "No book selected.";
                    return;
                }

                int bookId = SelectedBook.BookId;

                await Task.Run(() => model.DeleteBook(bookId));

                ClearEditor();
                StatusMessage = "Book deleted successfully.";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private async Task BorrowBookAsync()
        {
            try
            {
                if (SelectedBook == null || SelectedReader == null)
                {
                    StatusMessage = "Select a book and a reader.";
                    return;
                }

                int bookId = SelectedBook.BookId;
                int readerId = SelectedReader.ReaderId;

                await Task.Run(() => model.BorrowBook(bookId, readerId));

                StatusMessage = "Book borrowed successfully.";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private async Task ReturnBookAsync()
        {
            try
            {
                if (SelectedBook == null)
                {
                    StatusMessage = "No book selected.";
                    return;
                }

                int bookId = SelectedBook.BookId;

                await Task.Run(() => model.ReturnBook(bookId));

                StatusMessage = "Book returned successfully.";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        private bool CanAddBook()
        {
            return !string.IsNullOrWhiteSpace(NewTitle)
                && !string.IsNullOrWhiteSpace(NewAuthor)
                && !string.IsNullOrWhiteSpace(NewPublicationYear);
        }

        private bool CanEditSelectedBook()
        {
            return SelectedBook != null;
        }

        private bool CanBorrowBook()
        {
            return SelectedBook != null
                && SelectedReader != null
                && SelectedBook.IsAvailable;
        }

        private bool CanReturnBook()
        {
            return SelectedBook != null
                && !SelectedBook.IsAvailable;
        }

        private void CopySelectedBookToEditor()
        {
            if (SelectedBook == null)
            {
                return;
            }

            NewTitle = SelectedBook.Title;
            NewAuthor = SelectedBook.Author;
            NewPublicationYear = SelectedBook.PublicationYear.ToString();
        }

        private void ClearEditor()
        {
            SelectedBook = null;
            NewTitle = string.Empty;
            NewAuthor = string.Empty;
            NewPublicationYear = string.Empty;
            RaiseCommandStates();
        }

        private void RaiseCommandStates()
        {
            if (AddBookCommand != null)
            {
                AddBookCommand.RaiseCanExecuteChanged();
            }

            if (UpdateBookCommand != null)
            {
                UpdateBookCommand.RaiseCanExecuteChanged();
            }

            if (DeleteBookCommand != null)
            {
                DeleteBookCommand.RaiseCanExecuteChanged();
            }

            if (BorrowBookCommand != null)
            {
                BorrowBookCommand.RaiseCanExecuteChanged();
            }

            if (ReturnBookCommand != null)
            {
                ReturnBookCommand.RaiseCanExecuteChanged();
            }
        }

        private static void ReplaceCollection<T>(ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();

            foreach (T item in items)
            {
                collection.Add(item);
            }
        }
    }
}