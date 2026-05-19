using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologiesTask2.Data.Repositories;
using ProgrammingTechnologiesTask2.Logic.Services;
using ProgrammingTechnologiesTask2.Presentation.Model.Models;
using ProgrammingTechnologiesTask2.Presentation.ViewModel.ViewModels;
using ProgrammingTechnologiesTask2.Tests.TestData;

namespace ProgrammingTechnologiesTask2.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void LoadDataCommand_ShouldLoadBooksIntoViewModel()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);
            LibraryPresentationModel model = new LibraryPresentationModel(service);
            MainViewModel viewModel = new MainViewModel(model);

            viewModel.LoadDataCommand.Execute(null);

            bool loaded = WaitUntil(() => viewModel.Books.Count == 2, 2000);

            Assert.IsTrue(loaded);
            Assert.AreEqual(2, viewModel.Books.Count);
        }

        [TestMethod]
        public void SelectingBook_ShouldCopyBookDataToEditor()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);
            LibraryPresentationModel model = new LibraryPresentationModel(service);
            MainViewModel viewModel = new MainViewModel(model);

            viewModel.LoadDataCommand.Execute(null);
            WaitUntil(() => viewModel.Books.Count == 2, 2000);

            viewModel.SelectedBook = viewModel.Books[0];

            Assert.AreEqual(viewModel.Books[0].Title, viewModel.NewTitle);
            Assert.AreEqual(viewModel.Books[0].Author, viewModel.NewAuthor);
        }

        [TestMethod]
        public void AddBookCommand_ShouldAddBookThroughViewModel()
        {
            LibraryRepository repository = TestDataGenerator.CreateFakeRepositoryWithSampleData();
            LibraryService service = new LibraryServiceImplementation(repository);
            LibraryPresentationModel model = new LibraryPresentationModel(service);
            MainViewModel viewModel = new MainViewModel(model);

            viewModel.LoadDataCommand.Execute(null);
            WaitUntil(() => viewModel.Books.Count == 2, 2000);

            viewModel.NewTitle = "New Book";
            viewModel.NewAuthor = "New Author";
            viewModel.NewPublicationYear = "2025";

            viewModel.AddBookCommand.Execute(null);

            bool added = WaitUntil(() => viewModel.Books.Count == 3, 3000);

            Assert.IsTrue(added);
            Assert.AreEqual(3, viewModel.Books.Count);
        }

        private static bool WaitUntil(System.Func<bool> condition, int timeoutMilliseconds)
        {
            int waited = 0;

            while (waited < timeoutMilliseconds)
            {
                if (condition())
                {
                    return true;
                }

                Thread.Sleep(50);
                waited += 50;
            }

            return false;
        }
    }
}