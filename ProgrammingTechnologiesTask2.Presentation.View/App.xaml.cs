using System.Windows;
using ProgrammingTechnologiesTask2.Data.Repositories;
using ProgrammingTechnologiesTask2.Logic.Services;
using ProgrammingTechnologiesTask2.Presentation.Model.Models;
using ProgrammingTechnologiesTask2.Presentation.ViewModel.ViewModels;

namespace ProgrammingTechnologiesTask2.Presentation.View
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LibraryRepository repository = new SqlCompactLibraryRepository();
            LibraryService service = new LibraryServiceImplementation(repository);
            LibraryPresentationModel model = new LibraryPresentationModel(service);
            MainViewModel viewModel = new MainViewModel(model);

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();

            viewModel.LoadDataCommand.Execute(null);
        }
    }
}